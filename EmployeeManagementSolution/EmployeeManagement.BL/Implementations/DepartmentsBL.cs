using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;

namespace EmployeeManagement.BL.Implementations
{
    public class DepartmentsBL : IDepartmentsBL
    {
        private readonly EmployeesManagementDBContext _context;

        public DepartmentsBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> Get()
        {
            return _context.Departments.ToList();
        }
    }
}
