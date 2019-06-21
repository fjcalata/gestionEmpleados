using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IManageDatesBL
    {
        IEnumerable<DateTime> GetHolyDays();

        string[] GetArrayHolyDays();
    }
}
