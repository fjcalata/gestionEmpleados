using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IVacationsBL
    {
        bool Add(List<int> personsIds, List<DateTime> vacationsDates, Vacations vacation);

        void Delete(int id);

        IEnumerable<Vacations> Get();

        Vacations Get(int id);

        void Update(Vacations vacation);
    }
}
