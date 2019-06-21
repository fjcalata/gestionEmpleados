using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.BL.Implementations
{
    public class ExtensionTypesBL : IExtensionTypesBL
    {
        private readonly EmployeesManagementDBContext _context;

        public ExtensionTypesBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }

        public IEnumerable<ExtensionTypes> Get()
        {
            return _context.ExtensionTypes.ToList();
        }
    }
}
