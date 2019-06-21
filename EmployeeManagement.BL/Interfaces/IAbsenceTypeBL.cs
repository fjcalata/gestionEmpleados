using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Entities.Models;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IAbsenceTypeBL
    {
        IEnumerable<AbsenceType> Get();
    }
}
