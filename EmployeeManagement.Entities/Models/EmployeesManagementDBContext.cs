using System;
using EmployeeManagement.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeManagement.Entities.Models
{
    public partial class EmployeesManagementDBContext : DbContext
    {
        public EmployeesManagementDBContext()
        {
        }

        public EmployeesManagementDBContext(DbContextOptions<EmployeesManagementDBContext> options)
            : base(options)
        {
            this.Database.Migrate();
            this.Database.EnsureCreated();
        }

        public virtual DbSet<AbsenceType> AbsenceTypes { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<DayExtensions> DayExtensions { get; set; }
        public virtual DbSet<Degree> Degrees { get; set; }
        public virtual DbSet<DisplacementsDay> DisplacementsDays { get; set; }
        public virtual DbSet<DisplacementTypes> DisplacementTypes { get; set; }
        public virtual DbSet<ExtensionTypes> ExtensionTypes { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Trainings> Trainings { get; set; }
        public virtual DbSet<Vacations> Vacations { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<FunctionType> FunctionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            if (!optionsBuilder.IsConfigured)
            {

                //optionsBuilder.UseSqlServer("Server=Javier\\MSSQLSERVER01;Database=EmployeesManagementDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");
        }
    }
}
