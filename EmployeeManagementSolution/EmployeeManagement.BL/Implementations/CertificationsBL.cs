using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.BL.Implementations
{
    public class CertificationsBL : ICertificationsBL
    {
        private readonly EmployeesManagementDBContext _context;

        public CertificationsBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Certification> Get()
        {
            return _context.Certifications.ToList();
        }
    }
}
