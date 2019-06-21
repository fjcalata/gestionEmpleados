using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Entities.Migrations
{
    public partial class InitialCreae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbsenceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisplacementTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplacementTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtensionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModificatedAt = table.Column<DateTimeOffset>(nullable: false),
                    NIF = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    CorporationEmail = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Locality = table.Column<string>(nullable: false),
                    SocialSecurityNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayExtensions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModificatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ExtensionTypeId = table.Column<int>(nullable: true),
                    AffectationDate = table.Column<DateTime>(nullable: false),
                    HoursNumber = table.Column<int>(nullable: false),
                    PersonsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayExtensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayExtensions_ExtensionTypes_ExtensionTypeId",
                        column: x => x.ExtensionTypeId,
                        principalTable: "ExtensionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayExtensions_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DisplacementsDays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModificatedAt = table.Column<DateTimeOffset>(nullable: false),
                    DisplacementTypeId = table.Column<int>(nullable: true),
                    DisplacementDate = table.Column<DateTime>(nullable: false),
                    HoursNumber = table.Column<int>(nullable: false),
                    PersonsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplacementsDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DisplacementsDays_DisplacementTypes_DisplacementTypeId",
                        column: x => x.DisplacementTypeId,
                        principalTable: "DisplacementTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DisplacementsDays_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModificatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CertificationId = table.Column<int>(nullable: true),
                    DegreeId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsOfficial = table.Column<bool>(nullable: false),
                    PersonsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Certifications_CertificationId",
                        column: x => x.CertificationId,
                        principalTable: "Certifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainings_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainings_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    State = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    ModificatedAt = table.Column<DateTimeOffset>(nullable: false),
                    AbsenseTypeId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PersonsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_AbsenceTypes_AbsenseTypeId",
                        column: x => x.AbsenseTypeId,
                        principalTable: "AbsenceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vacations_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayExtensions_ExtensionTypeId",
                table: "DayExtensions",
                column: "ExtensionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DayExtensions_PersonsId",
                table: "DayExtensions",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplacementsDays_DisplacementTypeId",
                table: "DisplacementsDays",
                column: "DisplacementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplacementsDays_PersonsId",
                table: "DisplacementsDays",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CertificationId",
                table: "Trainings",
                column: "CertificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_DegreeId",
                table: "Trainings",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_PersonsId",
                table: "Trainings",
                column: "PersonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_AbsenseTypeId",
                table: "Vacations",
                column: "AbsenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_PersonsId",
                table: "Vacations",
                column: "PersonsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayExtensions");

            migrationBuilder.DropTable(
                name: "DisplacementsDays");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Vacations");

            migrationBuilder.DropTable(
                name: "ExtensionTypes");

            migrationBuilder.DropTable(
                name: "DisplacementTypes");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "AbsenceTypes");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
