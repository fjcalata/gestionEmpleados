using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Entities.Models;
using EmployeeManagement.BL.Interfaces;

namespace EmployeeManagement.BL.Implementations
{
    public class AbsenceTypeBL : IAbsenceTypeBL
    {
        private readonly EmployeesManagementDBContext _context;

        public AbsenceTypeBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }
        public IEnumerable<AbsenceType> Get()
        {
            return _context.AbsenceTypes.ToList();
        }
    }
}
