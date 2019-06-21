using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmployeeManagement.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.BL.Implementations
{
    public class DayExtensionsBL : IDayExtensionsBL
    {
        private readonly EmployeesManagementDBContext _context;

        private readonly IConfiguration _configuration;

        private readonly IValidationService _validationService;

        public List<DayExtensions> Get()
        {
            return _context.DayExtensions.ToList();
        }

        public DayExtensionsBL(EmployeesManagementDBContext context, IConfiguration configuration, IValidationService validationService)
        {
            _context = context;
            _configuration = configuration;
            _validationService = validationService;
        }

        public void Add(IEnumerable<int> listPersonId, DayExtensions dayExtension)
        {
            var persons = _context.Persons.Include(p => p.DayExtensions).Where(p => listPersonId.Contains(p.Id));
            var extensionType =
                _context.ExtensionTypes.FirstOrDefault(e => e.Id.Equals(dayExtension.ExtensionTypeId));
            dayExtension.CreatedAt = DateTimeOffset.Now;
            foreach (var person in persons){
                
                if (!ValidateMaxHours(person.DayExtensions
                    .Where(d => d.AffectationDate.Equals(dayExtension.AffectationDate)).Sum(d => d.HoursNumber) + dayExtension.HoursNumber))
                {
                    var newDayExtension = GetNewInstance(dayExtension, extensionType);
                    person.DayExtensions.Add(newDayExtension);
                    _context.Update(person);
                }
                else
                {
                    _validationService.Add("HoursNumber", Enumerations.ErrorType.Warning,
                        string.Format(ErrorMessages.MaxHours, $"{person.Name} {person.LastName}"));
                }

                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dayExtension = _context.DayExtensions.FirstOrDefault(d => d.Id.Equals(id));
            if (!dayExtension.AffectationDate.IsPreviousDate())
            {
                _context.DayExtensions.Remove(dayExtension);
                _context.SaveChanges();
            }
            else
            {
                _validationService.Add("AffectationDate", Enumerations.ErrorType.Error, ErrorMessages.PreviousDate);
            }
        }

        private bool ValidateMaxHours(int count)
        {
            var ExtensionMaxHours = int.Parse(_configuration.GetSection("Extensions:MaxHoursByDay").Value);
            return count > ExtensionMaxHours;
        }

        public DayExtensions GetNewInstance(DayExtensions dayExtensions, ExtensionTypes extensionTypes)
        {
            return new DayExtensions
            {
                AffectationDate = dayExtensions.AffectationDate,
                CreatedAt = DateTimeOffset.Now,
                ExtensionType = extensionTypes,
                ExtensionTypeId = dayExtensions.ExtensionTypeId,
                HoursNumber = dayExtensions.HoursNumber,
                PersonsId = dayExtensions.PersonsId,
                State = dayExtensions.State,
                RequestDate = DateTime.Now,
                ModificatedAt = DateTimeOffset.Now,
            };
        }
    }
}
