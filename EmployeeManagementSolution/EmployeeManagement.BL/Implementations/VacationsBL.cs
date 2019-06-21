using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nager.Date.PublicHolidays;

namespace EmployeeManagement.BL.Implementations
{
    public class VacationsBL : IVacationsBL
    {
        private readonly EmployeesManagementDBContext _context;

        private readonly IConfiguration _configuration;

        private readonly IValidationService _validationService;

        public VacationsBL(EmployeesManagementDBContext context, IConfiguration configuration, IValidationService validationService)
        {
            _context = context;
            _configuration = configuration;
            _validationService = validationService;
        }

        public bool Add(List<int> personsIds, List<DateTime> vacationsDates, Vacations vacation)
        {
            if (!vacationsDates.Any())
            {
                _validationService.Add("Date", Enumerations.ErrorType.Error, ErrorMessages.DatesRequired);
                return false;
            }

            try
            {
                var persons = _context.Persons.Include(p => p.Vacations).Where(p => personsIds.Contains(p.Id)).ToList();
                var absenseType = _context.AbsenceTypes.FirstOrDefault(a => a.Id.Equals(vacation.AbsenseTypeId));

                foreach (var person in persons)
                {
                    if (!ValidateMaxDays(person.Vacations.Count(v => v.Date.Year.Equals(vacation.Date.Year))))
                    {
                        foreach (var date in vacationsDates)
                        {
                            if (!date.ValidateExistingDate(person.Vacations.Select(v => v.Date)))
                            {
                                var newVacation = GetNewInstance(vacation, absenseType, date);
                                person.Vacations.Add(newVacation);
                                _context.Update(person);
                            }
                            else
                            {
                                _validationService.Add("Date", Enumerations.ErrorType.Error,
                                    string.Format(ErrorMessages.ExistingDateBulk, $"{person.Name} {person.LastName}", date.ToShortDateString()));
                            }
                        }
                    }
                    else
                    {
                        _validationService.Add("Date", Enumerations.ErrorType.Error, string.Format(ErrorMessages.MaxDays, $"{person.Name} {person.LastName}"));
                    }
                }

                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                _validationService.Add("Date", Enumerations.ErrorType.Error, (ex.InnerException ?? ex).Message);
                return false;
            }
        }

        public void Delete(int id)
        {
            var vacation = _context.Vacations.FirstOrDefault(v => v.Id.Equals(id));
            if (!vacation.Date.IsPreviousDate())
            {
                _context.Vacations.Remove(vacation);
                _context.SaveChanges();
            }
            else
            {
                _validationService.Add("Date", Enumerations.ErrorType.Error, ErrorMessages.PreviousDate);
            }
        }

        public IEnumerable<Vacations> Get()
        {
            return _context.Vacations.ToList();
        }

        public Vacations Get(int id)
        {
            return _context.Vacations.FirstOrDefault(v => v.Id.Equals(id));
        }

        public void Update(Vacations vacation)
        {
            var person = _context.Persons.Include(p => p.Vacations).AsNoTracking().FirstOrDefault(p => p.Id.Equals(vacation.PersonsId));
            if (!vacation.Date.ValidateExistingDate(person.Vacations.Select(v=>v.Date)))
            {
                vacation.AbsenseType = _context.AbsenceTypes.FirstOrDefault(a => a.Id.Equals(vacation.AbsenseTypeId));
                vacation.ModificatedAt = DateTimeOffset.Now;
                
                _context.Update(vacation);
                _context.SaveChanges();
            }
            else
            {
                _validationService.Add("Date", Enumerations.ErrorType.Error,
                    string.Format(ErrorMessages.ExistingDate, $"{person.Name} {person.LastName}"));
            }
        }

        private bool ValidateMaxDays(int count)
        {
            var vacationsMaxDays = int.Parse(_configuration.GetSection("Vacations:MaxDaysByYear").Value);
            return count > vacationsMaxDays;
        }

        private Vacations GetNewInstance(Vacations vacations, AbsenceType absenceType, DateTime date)
        {
            return new Vacations
            {
                AbsenseType = absenceType,
                AbsenseTypeId = vacations.AbsenseTypeId,
                CreatedAt = DateTimeOffset.Now,
                Date = date,
                State = vacations.State
            };
        }
    }
}
