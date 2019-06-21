using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class TrainingsController : Controller
    {
        private readonly ITrainingsBL _trainingBL;

        public TrainingsController(ITrainingsBL trainingBL)
        {
            _trainingBL = trainingBL;
        }

        public IActionResult Index()
        {
            return View();
        }
                
        public JsonResult Get([DataSourceRequest]DataSourceRequest request,int personId)
        {
            var trainings = _trainingBL.GetByPerson(personId);

            DataSourceResult result = trainings.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpPost]
        public JsonResult Add([DataSourceRequest]DataSourceRequest request,int personId, Trainings model)
        {
            if (ModelState.IsValid)
            {
                var newPerson =  _trainingBL.Add(personId,model);
                model = newPerson;
            }           

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public JsonResult Edit([DataSourceRequest]DataSourceRequest request, int personId, Trainings model)
        {
            if (ModelState.IsValid)
            {
                _trainingBL.Update(model);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));           
        }

        [HttpPost]
        public JsonResult Delete([DataSourceRequest]DataSourceRequest request, int personId, Trainings model)
        {
            if (ModelState.IsValid)
            {
                _trainingBL.Delete(model.Id);
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}