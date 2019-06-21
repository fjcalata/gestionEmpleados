using EmployeeManagement.BL.Interfaces;
using EmployeeManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeManagement.BL.Implementations
{
    public class TrainingsBL : ITrainingsBL
    {
        private readonly EmployeesManagementDBContext _context;

        public TrainingsBL(EmployeesManagementDBContext context)
        {
            _context = context;
        }

        public Trainings Add(int personId, Trainings training)
        {
            var person = _context.Persons.Include(p=>p.Trainings).FirstOrDefault(p => p.Id.Equals(personId));
            training.Certification = _context.Certifications.FirstOrDefault(c => c.Id.Equals(training.CertificationId));            
            training.CreatedAt = DateTimeOffset.Now;
            person.Trainings.Add(training);
            _context.Persons.Update(person);
            _context.SaveChanges();

            return training;
        }

        public void Delete(int id)
        {
            var training = _context.Trainings.FirstOrDefault(t => t.Id.Equals(id));
            _context.Trainings.Remove(training);
            _context.SaveChanges();
        }

        public IEnumerable<Trainings> Get()
        {
            return _context.Trainings.ToList();
        }

        public Trainings Get(int id)
        {
            return _context.Trainings.FirstOrDefault(t => t.Id.Equals(id));
        }

        public IEnumerable<Trainings> GetByPerson(int personId)
        {
            var person = _context.Persons.Include(p => p.Trainings).FirstOrDefault(p => p.Id.Equals(personId));//.Select(p => p.Trainings);
            _context.Entry(person).Collection(t => t.Trainings).Query().Include(t=>t.Certification).Load();            
            return person.Trainings;
        }

        public void Update(Trainings training)
        {            
            training.Certification = _context.Certifications.FirstOrDefault(c => c.Id.Equals(training.CertificationId));
            training.ModificatedAt = DateTimeOffset.Now;
            _context.Trainings.Update(training);
            _context.SaveChanges();
        }
    }
}
