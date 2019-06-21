using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Common;
using EmployeeManagement.Entities.Models;

namespace EmployeeManagement.BL.Implementations
{
    public class ValidationService : IValidationService
    {
        private Dictionary<string, List<Error>> _errors;

        public ValidationService()
        {
            _errors = new Dictionary<string, List<Error>>();
        }        

        public void Add(string key, Enumerations.ErrorType type, string message)
        {
            var newError = new Error {Type = type, Message = message};

            if(_errors.ContainsKey(key))
            {
                _errors[key].Add(newError);
            }
            else
            {
                var listError = new List<Error>();
                listError.Add(newError);
                _errors.Add(key, listError);
            }
        }

        public Dictionary<string, List<Error>> GetErrors()
        {
            return _errors;
        }

        public bool HasErrors()
        {
            return _errors.Count > 0;
        }
    }
}
