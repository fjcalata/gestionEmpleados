using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EmployeeManagement.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.BL.Implementations
{
    public class DisplacementsDaysBL : IDisplacementsDaysBL
    {
        private readonly EmployeesManagementDBContext _context;

        private readonly IConfiguration _configuration;

        private readonly IValidationService _validationService;

        public DisplacementsDaysBL(EmployeesManagementDBContext context, IConfiguration configuration, IValidationService validationService)
        {
            _context = context;
            _configuration = configuration;
            _validationService = validationService;
        }

        public bool Add(List<int> personsId, DisplacementsDay displacementsDay)
        {
            var persons = _context.Persons.Include(p=>p.DisplacementsDays).Where(p => personsId.Contains(p.Id)).ToList();
            var displacementType = _context.DisplacementTypes.FirstOrDefault(d => d.Id.Equals(displacementsDay.DisplacementTypeId));
            foreach (var person in persons)
            {                
                if (!ValidateMaxHours(person.DisplacementsDays.Where(d => d.DisplacementDate.Equals(displacementsDay.DisplacementDate)).Sum(d => d.HoursNumber) + displacementsDay.HoursNumber))
                {
                    var newDisplacement = GetNewInstance(displacementsDay, displacementType);
                    person.DisplacementsDays.Add(newDisplacement);
                    _context.Update(person);
                }
                else
                {
                    _validationService.Add("HoursNumber", Enumerations.ErrorType.Warning,
                        string.Format(ErrorMessages.MaxHours, $"{person.Name} {person.LastName}"));
                }
            }
            
            _context.SaveChanges();

            return true;
        }

        public void Delete(int id)
        {
            var displacementDay = _context.DisplacementsDays.FirstOrDefault(d => d.Id.Equals(id));
            if (!displacementDay.DisplacementDate.IsPreviousDate())
            {
                _context.Remove(displacementDay);
                _context.SaveChanges();
            }
            else
            {
                _validationService.Add("DisplacementDate", Enumerations.ErrorType.Error, ErrorMessages.PreviousDate);
            }
        }

        public IEnumerable<DisplacementsDay> Get()
        {
            return _context.DisplacementsDays.ToList();
        }

        public void Update(DisplacementsDay displacementsDay)
        {
            displacementsDay.DisplacementType = _context.DisplacementTypes.FirstOrDefault(d => d.Id.Equals(displacementsDay.DisplacementTypeId));
            displacementsDay.ModificatedAt = DateTimeOffset.Now;
            _context.DisplacementsDays.Update(displacementsDay);
            _context.SaveChanges();
        }

        private bool ValidateMaxHours(int count)
        {
            var displacementsMaxHours = int.Parse(_configuration.GetSection("Displacements:MaxHoursByDay").Value);
            return count > displacementsMaxHours;
        }

        private DisplacementsDay GetNewInstance(DisplacementsDay displacementsDay, DisplacementTypes displacementTypes)
        {
            return new DisplacementsDay
            {
                CreatedAt = DateTimeOffset.Now,
                DisplacementDate = displacementsDay.DisplacementDate,
                DisplacementType = displacementTypes,
                DisplacementTypeId = displacementsDay.DisplacementTypeId,
                HoursNumber = displacementsDay.HoursNumber,
                State = displacementsDay.State
            };
        }
    }
}
