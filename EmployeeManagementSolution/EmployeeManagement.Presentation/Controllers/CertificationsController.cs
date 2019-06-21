using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class CertificationsController : Controller
    {
        private readonly ICertificationsBL _certificationsBL;

        public CertificationsController(ICertificationsBL certificationsBL)
        {
            _certificationsBL = certificationsBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var certifications = _certificationsBL.Get();
           
            return Json(certifications);
        }        
    }
}