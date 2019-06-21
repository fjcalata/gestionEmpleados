using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Entities.Models
{
    public abstract class BaseModel
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [Display(Name = "Estado")]
        [ScaffoldColumn(false)]
        public bool State { get; set; }

        [ScaffoldColumn(false)]
        public DateTimeOffset CreatedAt { get; set; }

        [ScaffoldColumn(false)]        
        public DateTimeOffset ModificatedAt { get; set; }
    }
}
