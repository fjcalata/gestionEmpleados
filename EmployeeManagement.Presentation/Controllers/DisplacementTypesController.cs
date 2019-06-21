using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class DisplacementTypesController : Controller
    {
        private readonly IDisplacementTypesBL _displacementTypesBL;

        public DisplacementTypesController(IDisplacementTypesBL displacementTypesBL)
        {
            _displacementTypesBL = displacementTypesBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var displacementTypesBL = _displacementTypesBL.Get();
           
            return Json(displacementTypesBL);
        }        
    }
}