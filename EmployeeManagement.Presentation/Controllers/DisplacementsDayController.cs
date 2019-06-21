using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.Presentation.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class DisplacementsDayController : Controller
    {
        private readonly IDisplacementsDaysBL _displacementsDaysBL;
        private readonly IValidationService _validationService;

        public DisplacementsDayController(IDisplacementsDaysBL displacementsDaysBL, IValidationService validationService)
        {
            _displacementsDaysBL = displacementsDaysBL;
            _validationService = validationService;
        }

        public IActionResult Index()
        {
            return View();
        }
                
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var displacementsDays = _displacementsDaysBL.Get();

            DataSourceResult result = displacementsDays.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Add([DataSourceRequest]DataSourceRequest request, List<int> personsIds, DisplacementsDay model)
        {
            ModelState.Remove("PersonsId");

            if (ModelState.IsValid)
            {
                _displacementsDaysBL.Add(personsIds, model);

                ModelState.AddError(_validationService);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public JsonResult Delete([DataSourceRequest] DataSourceRequest request, DisplacementsDay model)
        {
            _displacementsDaysBL.Delete(model.Id);

            ModelState.AddError(_validationService);

            return Json(ModelState.ToDataSourceResult());
        }
    }
}