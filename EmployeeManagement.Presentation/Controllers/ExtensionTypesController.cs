using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class ExtensionTypesController : Controller
    {
        private readonly IExtensionTypesBL _extensionTypesBL;

        public ExtensionTypesController(IExtensionTypesBL extensionTypesBL)
        {
            _extensionTypesBL = extensionTypesBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var extensionTypes = _extensionTypesBL.Get();
           
            return Json(extensionTypes);
        }        
    }
}