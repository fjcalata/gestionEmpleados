using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;

namespace EmployeeManagement.BL.Implementations
{
    public class FunctionTypesBL : IFunctionTypesBL
    {
        private readonly EmployeesManagementDBContext _context;

        public FunctionTypesBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }

        public IEnumerable<FunctionType> Get()
        {
            return _context.FunctionTypes.ToList();
        }
    }
}
