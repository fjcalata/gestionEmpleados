using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IDisplacementsDaysBL
    {
        IEnumerable<DisplacementsDay> Get();

        bool Add(List<int> personsId, DisplacementsDay displacementsDay);

        void Delete(int id);

        void Update(DisplacementsDay displacementsDay);
    }
}
