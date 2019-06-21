using EmployeeManagement.Entities.Helpers.Validations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities.Models
{
    public class Trainings : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int PersonsId { get; set; }

        [Display(Name = "Titulación")]
        [UIHint("ucCboCertifications")]
        public Nullable<int> CertificationId { get; set; }

        [Display(Name = "Estudio")]        
        public string Degree { get; set; }              

        [ForeignKey("CertificationId")]
        public Certification Certification { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de finalización")]
        [DataType(DataType.Date)]
        [DateLessThan("StartDate", ErrorMessage = "La {0} no puede ser superior a la fecha de inicio.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Es oficial")]
        public bool IsOfficial { get; set; }       
    }
}
