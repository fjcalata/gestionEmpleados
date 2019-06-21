using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class DegreesController : Controller
    {
        private readonly IDegreesBL _degreesBL;

        public DegreesController(IDegreesBL degreesBL)
        {
            _degreesBL = degreesBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var certifications = _degreesBL.Get();
           
            return Json(certifications);
        }        
    }
}