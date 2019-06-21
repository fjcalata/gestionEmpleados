using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class FunctionTypesController : Controller
    {
        private readonly IFunctionTypesBL _functionTypesBL;

        public FunctionTypesController(IFunctionTypesBL functionTypesBL)
        {
            _functionTypesBL = functionTypesBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var model = _functionTypesBL.Get();
           
            return Json(model);
        }        
    }
}