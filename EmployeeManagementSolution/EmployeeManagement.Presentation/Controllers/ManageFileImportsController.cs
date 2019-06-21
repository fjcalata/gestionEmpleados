using EmployeeManagement.BL.Implementations;
using EmployeeManagement.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class ManageFileImportsController : Controller
    {
        private readonly IManageFiles _namageFiles;
        private readonly IPersonsBL _persons;

        public ManageFileImportsController(IManageFiles namageFiles, IPersonsBL persons)
        {
            _namageFiles = namageFiles;
            _persons = persons;
        }

        public IActionResult ImportView()
        {
            return View();
        }

        public IActionResult ImportFiles(IEnumerable<IFormFile> files)
        {
            try
            {
                IEnumerable<string> fileInfo = new List<string>();

                if (files != null)
                {
                    fileInfo = ImportFilesData(files);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }            

            return Json(new { success = true });
        }

        private IEnumerable<string> ImportFilesData(IEnumerable<IFormFile> files)
        {
            List<string> fileInfo = new List<string>();

            foreach (var file in files)
            {
                var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));

                _persons.AddImport(file.OpenReadStream());
                //using (var ms = new MemoryStream())
                //{
                //    file.CopyTo(ms);

                //    _namageFiles.SavePersonsFile(ms);                    
                //}
                //_namageFiles.SavePersonsFile(file.OpenReadStream());                

                //fileInfo.Add(string.Format("{0} ({1} bytes)", fileName, file.Length));
            }

            return fileInfo;
        }
    }
}