using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.BL.Implementations
{
    public class DisplacementsTypesBL : IDisplacementTypesBL
    {
        private readonly EmployeesManagementDBContext _context;

        public DisplacementsTypesBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }

        public IEnumerable<DisplacementTypes> Get()
        {
            return _context.DisplacementTypes.ToList();
        }
    }
}
