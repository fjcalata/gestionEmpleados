using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace EmployeeManagement.BL.Implementations
{
    public class PersonsBL : IPersonsBL
    {
        private readonly EmployeesManagementDBContext _context;

        private readonly IValidationService _validationService;

        private readonly IManageFiles _manageFiles;

        public PersonsBL(EmployeesManagementDBContext context, IValidationService validationService, IManageFiles manageFiles)
        {
            _context = context;
            _validationService = validationService;
            _manageFiles = manageFiles;
        }

        public async Task<int> Add(Persons person)
        {
            person.CreatedAt = DateTimeOffset.Now;
            person.Department = _context.Departments.FirstOrDefault(d => d.Id.Equals(person.DepartmentsId));
            person.FunctionType = _context.FunctionTypes.FirstOrDefault(f => f.Id.Equals(person.FunctionTypesId));
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return person.Id;
        }

        public void Update(Persons person)
        {
            person.ModificatedAt = DateTimeOffset.Now;
            _context.Persons.Update(person);
            _context.SaveChanges();
        }

        public List<Persons> Get()
        {
            return _context.Persons.ToList();
        }

        public Persons Get(int id)
        {
            return _context.Persons.FirstOrDefault(p => p.Id.Equals(id));
        }

        public IEnumerable<object> GetIdsAndNames()
        {
            return _context.Persons.Select(p => new { p.Id, Name = $"{p.Name} {p.LastName}"});
        }

        public bool Login(string user, string password)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Password.Equals(password) && p.User.Equals(user));
            if (person != null)
            {
                return true;
            }
            else
            {
                _validationService.Add(string.Empty, Entities.Common.Enumerations.ErrorType.Error, ErrorMessages.UserNotExist);
                return false;
            }

        }

        public void AddImport(Stream stream)
        {
            var persons = _manageFiles.loadCsvFile(stream);
            _context.Persons.AddRange(persons);
            // Debo comprobar que todo ok, ya que cambia lo que hay
            _context.SaveChanges();
        }
    }
}
