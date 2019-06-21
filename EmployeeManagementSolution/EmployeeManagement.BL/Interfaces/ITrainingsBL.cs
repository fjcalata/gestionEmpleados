using EmployeeManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.BL.Interfaces
{
    public interface ITrainingsBL
    {
        Trainings Add(int personId, Trainings training);

        void Delete(int id);

        IEnumerable<Trainings> Get();

        Trainings Get(int id);

        IEnumerable<Trainings> GetByPerson(int personId)
;
        void Update(Trainings training);
    }
}
