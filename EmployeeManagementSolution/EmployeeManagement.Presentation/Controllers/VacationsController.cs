using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Presentation.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class VacationsController : Controller
    {
        private readonly IVacationsBL _vacationsBL;
        private readonly IValidationService _validationService;

        public VacationsController(IVacationsBL vacationsBL, IValidationService validationService)
        {
            _vacationsBL = vacationsBL;
            _validationService = validationService;
        }

        public IActionResult Index()
        {
            return View();
        }
                
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var vacations = _vacationsBL.Get().ToList();

            DataSourceResult result = vacations.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Add([DataSourceRequest]DataSourceRequest request, List<int> personsIds,List<DateTime> vacationDates, Vacations model)
        {
            ModelState.Remove("PersonsId");
            ModelState.Remove("Date");

            if (model != null && ModelState.IsValid)
            {
                var state = _vacationsBL.Add(personsIds, vacationDates, model);

                ModelState.AddError(_validationService);              
            }            

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public JsonResult Edit([DataSourceRequest]DataSourceRequest request, int personsId, Vacations model)
        {
            if (model != null)
            {
                model.PersonsId = personsId;

                if (ModelState.IsValid)
                {
                    _vacationsBL.Update(model);
                }
            }

            ModelState.AddError(_validationService);

            return Json(ModelState.ToDataSourceResult());
        }

        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, Vacations model)
        {
            if (model != null)
            {
                _vacationsBL.Delete(model.Id);
            }

            ModelState.AddError(_validationService);

            return Json(ModelState.ToDataSourceResult());
        }
    }
}