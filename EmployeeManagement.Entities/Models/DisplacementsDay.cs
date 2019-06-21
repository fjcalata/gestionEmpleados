using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities.Models
{
    public class DisplacementsDay : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Empleado")]
        [UIHint("ucPersonsEditor")]
        public int PersonsId { get; set; }

        [Display(Name = "Tipo de desplazamiento")]
        [UIHint("ucCboDisplacementTypes")]
        public Nullable<int> DisplacementTypeId { get; set; }

        [ForeignKey("DisplacementTypeId")]
        public virtual DisplacementTypes DisplacementType { get; set; }

        [Display(Name = "Fecha")]
        [UIHint("ucDateWorkingDays")]
        public DateTime DisplacementDate { get; set; }

        [Display(Name = "Cantidad de horas")]
        [UIHint("ucNumberLimitedMaxMin")]
        public int HoursNumber { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public bool IsPreviousDate
        {
            get
            {
                return DisplacementDate < DateTime.Now;
            }
        }

    }
}
