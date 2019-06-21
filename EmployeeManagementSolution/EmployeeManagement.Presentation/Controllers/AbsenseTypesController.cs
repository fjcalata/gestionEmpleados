using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class AbsenseTypesController : Controller
    {
        private readonly IAbsenceTypeBL _absenceTypeBL;

        public AbsenseTypesController(IAbsenceTypeBL absenceTypeBL)
        {
            _absenceTypeBL = absenceTypeBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var absenceTypeBL = _absenceTypeBL.Get();
           
            return Json(absenceTypeBL);
        }        
    }
}