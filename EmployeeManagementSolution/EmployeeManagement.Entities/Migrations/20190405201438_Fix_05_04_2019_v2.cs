using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Entities.Migrations
{
    public partial class Fix_05_04_2019_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FunctionTypesId",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DepartmentsId",
                table: "Persons",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FunctionTypesId",
                table: "Persons",
                column: "FunctionTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Departments_DepartmentsId",
                table: "Persons",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_FunctionTypes_FunctionTypesId",
                table: "Persons",
                column: "FunctionTypesId",
                principalTable: "FunctionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.InsertData(
               table: "FunctionTypes",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { 1, "Coordinador Servicio" },
                    { 2, "Coordinador Área" },
                    { 3, "Técnico" },
                    { 4, "Becario" },
                    { 5, "Auxiliar" }
               });

            migrationBuilder.InsertData(
               table: "Departments",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { 1, "Radio" },
                    { 2, "Televisión" },
                    { 3, "Transmedia" }                    
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Departments_DepartmentsId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_FunctionTypes_FunctionTypesId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "FunctionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DepartmentsId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_FunctionTypesId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FunctionTypesId",
                table: "Persons");
        }
    }
}
