using EmployeeManagement.BL.Interfaces;
using System;
using System.Collections.Generic;
using Nager.Date;
using System.Linq;

namespace EmployeeManagement.BL.Implementations
{
    public class ManageDatesBL : IManageDatesBL
    {
        public IEnumerable<DateTime> GetHolyDays()
        {
            return GetAllHolidays();
        }

        public string[] GetArrayHolyDays()
        {
            return GetAllHolidays().Select(d => d.ToShortDateString()).ToArray();
        }

        private IEnumerable<DateTime> GetAllHolidays()
        {
            var result = DateSystem.GetPublicHoliday(CountryCode.ES, DateTime.Now.Year).Select(d => d.Date).ToList();
            result.AddRange(DateSystem.GetPublicHoliday(CountryCode.ES, DateTime.Now.AddYears(1).Year).Select(d => d.Date).ToList());
            return result;
        }
    }
}
