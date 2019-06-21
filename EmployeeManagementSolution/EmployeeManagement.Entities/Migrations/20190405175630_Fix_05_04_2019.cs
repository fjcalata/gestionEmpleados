using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Entities.Migrations
{
    public partial class Fix_05_04_2019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
               name: "IX_Trainings_DegreeId",
               table: "Trainings");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Degrees_DegreeId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "Trainings");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "Trainings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDate",
                table: "DayExtensions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "RequestDate",
                table: "DayExtensions");

            migrationBuilder.AddColumn<int>(
                name: "DegreeId",
                table: "Trainings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_DegreeId",
                table: "Trainings",
                column: "DegreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Degrees_DegreeId",
                table: "Trainings",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
