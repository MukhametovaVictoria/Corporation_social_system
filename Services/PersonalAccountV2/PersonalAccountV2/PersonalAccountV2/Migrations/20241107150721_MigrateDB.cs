using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAccountV2.Migrations
{
    /// <inheritdoc />
    public partial class MigrateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("8094d7d3-20e6-4604-88a1-75d7b0a96981")),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(7315)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(7805)),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainTelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmploymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accomplishment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("664e7099-0007-41ca-bc54-02f6375c98b9")),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(8374)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(8702)),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomplishment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accomplishment_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Communication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("3d5d8c3a-51e5-41d8-87df-7550757a64de")),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(9254)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(9522)),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communication_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("37ef3607-5aa6-4dd8-8eb6-d18aa763f00d")),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(72)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(431)),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAcrive = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("0cd96cc4-deb0-4336-8e3b-943c69440d70")),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(963)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(1282)),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DismissalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescriptionWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("9f8d5f97-dbce-40c0-8981-c537fa4f53a6")),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(1809)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(2065)),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accomplishment_EmployeeId",
                table: "Accomplishment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Communication_EmployeeId",
                table: "Communication",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_EmployeeId",
                table: "Event",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_EmployeeId",
                table: "Experience",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_EmployeeId",
                table: "Skill",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accomplishment");

            migrationBuilder.DropTable(
                name: "Communication");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
