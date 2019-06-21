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

#pragma warning disable CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, DayExtensions model)
#pragma warning restore CS1998 // El método asincrónico carece de operadores "await" y se ejecutará de forma sincrónica. Puede usar el operador 'await' para esperar llamadas API que no sean de bloqueo o 'await Task.Run(...)' para hacer tareas enlazadas a la CPU en un subproceso en segundo plano.
        {
            _dayExtensionsBL.Delete(model.Id);

            ModelState.AddError(_validationService);

            return Json(ModelState.ToDataSourceResult());
        }
    }
}