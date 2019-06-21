using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Presentation.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class DayExtensionsController : Controller
    {
        private readonly IDayExtensionsBL _dayExtensionsBL;
        private readonly IValidationService _validationService;

        public DayExtensionsController(IDayExtensionsBL dayExtensionsBL, IValidationService validationService)
        {
            _dayExtensionsBL = dayExtensionsBL;
            _validationService = validationService;
        }

        public IActionResult Index()
        {
            return View();
        }
                
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var dayExtensions = _dayExtensionsBL.Get();

            DataSourceResult result = dayExtensions.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Add([DataSourceRequest]DataSourceRequest request, List<int> personsIds,  DayExtensions model)
        {
            ModelState.Remove("PersonsId");

            if (ModelState.IsValid)
            {
                _dayExtensionsBL.Add(personsIds, model);

                ModelState.AddError(_validationService);
            }           

            return Json(ModelState.ToDataSourceResult());
        }       

        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, DayExtensions model)
        {
            _dayExtensionsBL.Delete(model.Id);

            ModelState.AddError(_validationService);

            return Json(ModelState.ToDataSourceResult());
        }
    }
}