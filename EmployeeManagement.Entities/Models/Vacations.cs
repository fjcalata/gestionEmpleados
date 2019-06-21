using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities.Models
{
    public class Vacations : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }     

        [Display(Name = "Empleado")]        
        [UIHint("ucPersonsEditor")]
        public int PersonsId { get; set; }

        [Display(Name = "Tipo de ausencia")]
        [UIHint("ucCboAbsenseTypes")]
        public Nullable<int> AbsenseTypeId { get; set; }

        [ForeignKey("AbsenseTypeId")]
        public virtual AbsenceType AbsenseType { get; set; }

        [Display(Name = "Fecha")]
        [UIHint("ucDateWorkingMultipleDays")]
        public DateTime Date { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public bool IsPreviousDate
        {
            get
            {
                return Date < DateTime.Now;
            }
        }
    }
}
