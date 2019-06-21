using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PersonsController : Controller
    {
        private readonly IPersonsBL _personsBL;

        public PersonsController(IPersonsBL personsBL)
        {
            _personsBL = personsBL;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var persons = _personsBL.Get().ToList();

            DataSourceResult result = persons.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Add([DataSourceRequest]DataSourceRequest request, Persons model)
        {
            if (ModelState.IsValid)
            {
                var newPersonId = await _personsBL.Add(model);
                model.Id = newPersonId;
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public JsonResult Edit([DataSourceRequest]DataSourceRequest request, Persons model)
        {
            if (ModelState.IsValid)
            {
                _personsBL.Update(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));           
        }
    }
}