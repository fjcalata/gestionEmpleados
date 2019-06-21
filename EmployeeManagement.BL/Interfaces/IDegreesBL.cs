using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IDegreesBL
    {
        IEnumerable<Degree> Get();
    }
}
