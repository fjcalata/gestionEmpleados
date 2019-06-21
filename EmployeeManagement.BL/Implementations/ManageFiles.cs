using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;

namespace EmployeeManagement.BL.Implementations
{
    public class ManageFiles : IManageFiles
    {
        private readonly EmployeesManagementDBContext _context;

        public ManageFiles(EmployeesManagementDBContext context)
        {
            _context = context;
        }

        public List<Persons> loadCsvFile(Stream stream)
        {
            List<string> searchList = new List<string>();
            using (var reader = new StreamReader(stream))
            { 
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    searchList.Add(line);
                }
            }

            var persons = new List<Persons>();

            if (searchList.Count > 1)
            {
                searchList.Skip(1).ToList().ForEach(s =>
                {
                    var data = s.Split(';');
                    var functionType = _context.FunctionTypes.FirstOrDefault(f => f.Id.Equals(Convert.ToInt32(data[9])));
                    var department = _context.Departments.FirstOrDefault(d => d.Id.Equals(Convert.ToInt32(data[10])));
                    persons.Add(new Persons {
                        Name = data[0],
                        LastName = data[1],
                        NIF = data[2],
                        CorporationEmail = data[3],
                        Email = data[4],
                        Adress = data[5],
                        PostalCode = data[6],
                        Locality = data[7],
                        SocialSecurityNumber = data[8],
                        CreatedAt = DateTimeOffset.Now,
                        FunctionType = functionType,
                        Department = department                        
                    });
                });
                // Compruebo de no incluir personas que tenga ya incluidas y para eso elimino a las que tengan el mismo DNI
                persons.RemoveAll(d => _context.Persons.Select(p => p.NIF).ToList().Contains(d.NIF));
            }

            return persons;
        }
    }
}
