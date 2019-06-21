using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Presentation.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "El {0} es requerido")]        
        public string user { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string ReturnUrl { get; set; }
    }
}