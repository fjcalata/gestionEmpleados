using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities.Models
{
    public partial class Persons : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "Los {0} son requeridos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        public string NIF { get; set; }

        [DisplayName("Departamento")]
        [UIHint("ucCboDepartments")]
        public Nullable<int> DepartmentsId { get; set; }

        [ForeignKey("DepartmentsId")]
        public Department Department { get; set; }

        [DisplayName("Tipo de Función")]
        [UIHint("ucCboFunctionTypes")]
        public Nullable<int> FunctionTypesId { get; set; }

        [ForeignKey("FunctionTypesId")]
        public FunctionType FunctionType { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email corporativo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string CorporationEmail { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email personal")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string Email { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public string Adress { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = "Código postal")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public string PostalCode { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public string Locality { get; set; }

        [Display(Name = "Número S.S")]
        [Required(ErrorMessage = "La {0} es requerida")]
        public string SocialSecurityNumber { get; set; }

        [ScaffoldColumn(false)]
        public string User { get; set; }

        [ScaffoldColumn(false)]
        public string Password { get; set; }        
       
        public virtual List<Trainings> Trainings { get; set; }

        public virtual List<Vacations> Vacations { get; set; }

        public virtual List<DayExtensions> DayExtensions { get; set; }

        public virtual List<DisplacementsDay> DisplacementsDays { get; set; }
    }
}
