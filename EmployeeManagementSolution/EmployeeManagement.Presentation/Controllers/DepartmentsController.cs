using EmployeeManagement.BL.Interfaces;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Presentation.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsBL _departmentsBL;

        public DepartmentsController(IDepartmentsBL departmentsBL)
        {
            _departmentsBL = departmentsBL;
        }

        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var model= _departmentsBL.Get();
           
            return Json(model);
        }        
    }
}