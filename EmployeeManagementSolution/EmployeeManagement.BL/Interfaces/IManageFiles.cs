using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EmployeeManagement.Entities.Models;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IManageFiles
    {
        List<Persons> loadCsvFile(Stream stream);
    }
}
