using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeTracker",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracker_EmployeeId",
                table: "TimeTracker",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracker_ProjectId",
                table: "TimeTracker",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracker_Employee_EmployeeId",
                table: "TimeTracker",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTracker_Project_ProjectId",
                table: "TimeTracker",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracker_Employee_EmployeeId",
                table: "TimeTracker");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTracker_Project_ProjectId",
                table: "TimeTracker");

            migrationBuilder.DropIndex(
                name: "IX_TimeTracker_EmployeeId",
                table: "TimeTracker");

            migrationBuilder.DropIndex(
                name: "IX_TimeTracker_ProjectId",
                table: "TimeTracker");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TimeTracker",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
