using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.BL.Implementations
{
    public class DegreesBL : IDegreesBL
    {
        private readonly EmployeesManagementDBContext _context;

        public DegreesBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Degree> Get()
        {
            return _context.Degrees.ToList();
        }
    }
}
