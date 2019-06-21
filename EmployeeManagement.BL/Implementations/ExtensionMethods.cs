using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.BL.Implementations
{
    public static class ExtensionMethods
    {
        public static bool IsPreviousDate(this DateTime date)
        {
            var difference = date - DateTime.Now;
            return difference.TotalDays < 0;
        }

        public static bool ValidateExistingDate(this DateTime actualDate, IEnumerable<DateTime> existingDates)
        {
            return existingDates.Any(e => e.ToShortDateString().Equals(actualDate.ToShortDateString()));
        }
    }
}
