using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Entities.Common;

namespace EmployeeManagement.Entities.Models
{
    public class Error
    {
        public string Message { get; set; }

        public Enumerations.ErrorType Type { get; set; }
    }
}
