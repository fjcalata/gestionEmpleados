using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EmployeeManagement.Entities.Migrations
{
    public partial class AddUserAndPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Persons",
                nullable: true);

            migrationBuilder.InsertData(
                 table: "Persons",
                 columns: new[] { "Id", "State", "CreatedAt", "ModificatedAt", "NIF", "Name", "LastName", "CorporationEmail", "Email", "Adress", "PostalCode", "Locality", "SocialSecurityNumber", "Password", "User" },
                 values: new object[,]
                 {
                    { 1, true, DateTime.Now, DateTime.Now, "00000000", "Admin", "Admin", "Admin@Admin.com", "Admin@Admin.com", "calle asalto","50014","Zaragoza","100001","1234","admin" },
                 });

            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bachillerato" },
                    { 2, "Formación profesional grado medio" },
                    { 3, "Formación profesional grado superior" },
                    { 4, "Grado universitario" },
                    { 5, "Diplomatura" },
                    { 6, "Licenciatura" },
                    { 7, "Ingeniería técnica" },
                    { 8, "Ingeniería superior" },
                    { 9, "Máster" },
                    { 10, "Otros" }
                });

            migrationBuilder.InsertData(
                table: "AbsenceTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Vacaciones" },
                    { 2, "Moscoso" },
                    { 3, "Baja IT" },
                    { 4, "Baja maternal" },
                    { 5, "Ganado" }                    
                });

            migrationBuilder.InsertData(
                table: "DisplacementTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Adelanto" },
                    { 2, "Retraso" },                  
                });

            migrationBuilder.InsertData(
              table: "ExtensionTypes",
              columns: new[] { "Id", "Name" },
              values: new object[,]
              {
                    { 1, "Ampliación" },
                    { 2, "Reducción" },                  
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "User",
                table: "Persons");
        }
    }
}
