using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities.Models
{
    public class DayExtensions : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Empleado")]
        [UIHint("ucPersonsEditor")]
        public int PersonsId { get; set; }

        [Display(Name = "Solicitado por")]
        [UIHint("ucCboExtensionTypes")]
        public Nullable<int> ExtensionTypeId { get; set; }

        [ForeignKey("ExtensionTypeId")]
        public virtual ExtensionTypes ExtensionType { get; set; }

        [Display(Name = "Fecha de solicitud")]
        [UIHint("ucDateWorkingDays")]
        public DateTime RequestDate { get; set; }

        [Display(Name = "Fecha de afectación")]        
        [UIHint("ucDateWorkingDays")]
        public DateTime AffectationDate { get; set; }

        [Display(Name = "Cantidad de horas")]        
        [UIHint("ucNumberLimitedMaxMin", null,"Min","0","Max","8")]
        
        public int HoursNumber { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public bool IsPreviousDate
        {
            get
            {
                return AffectationDate < DateTime.Now;
            }
        }
    }
}
