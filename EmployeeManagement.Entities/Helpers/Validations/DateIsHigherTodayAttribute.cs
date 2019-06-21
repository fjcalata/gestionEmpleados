using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entities.Helpers.Validations
{
    public sealed class DateIsHigherTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;

            return (dateStart > DateTime.Now);
        }
    }
}
