using EmployeeManagement.Entities.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.BL.Interfaces
{
    public interface IPersonsBL
    {
        Task<int> Add(Persons persons);

        void AddImport(Stream stream);
        
        List<Persons> Get();

        Persons Get(int id);

        IEnumerable<object> GetIdsAndNames();

        void Update(Persons person);

        bool Login(string user, string password);
    }
}