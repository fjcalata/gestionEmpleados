using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.Entities.Common;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IValidationService
    {
        void Add(string key, Enumerations.ErrorType type, string message);

        Dictionary<string, List<Error>> GetErrors();

        bool HasErrors();
    }
}
