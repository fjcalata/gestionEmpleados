using EmployeeManagement.BL.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmployeeManagement.Presentation.Helpers
{
    public static class ExtensionMethods
    {
        public static void AddError(this ModelStateDictionary modelState, IValidationService validationService)
        {
            var errors = validationService.GetErrors();

            foreach (var error in errors)
            {
                foreach (var fieldErrors in error.Value)
                {
                    modelState.AddModelError(error.Key, fieldErrors.Message);
                }                
            }
        }
    }
}
