using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IDayExtensionsBL
    {
        void Add(IEnumerable<int> listPersonId, DayExtensions dayExtension);

        void Delete(int id);

        List<DayExtensions> Get();
    }
}
