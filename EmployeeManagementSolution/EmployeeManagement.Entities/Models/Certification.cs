using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Entities.Models
{   
    public class Certification
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
