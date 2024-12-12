using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAccountV2.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Skill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(8479),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(2065));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Skill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(8202),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(1809));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Skill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("7d3d7554-433a-4122-9b13-a05f57acabde"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("9f8d5f97-dbce-40c0-8981-c537fa4f53a6"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Experience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(7650),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(1282));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Experience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(7284),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(963));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("1776730a-be0d-44c7-b41d-5e5ee660fc9d"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("0cd96cc4-deb0-4336-8e3b-943c69440d70"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(6701),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(431));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(6412),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(72));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("73186bc0-9714-4dfd-aad5-cc1cdac206da"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("37ef3607-5aa6-4dd8-8eb6-d18aa763f00d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(3668),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(7805));

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OfficeAddress",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MainTelephoneNumber",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MainEmail",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentDate",
                table: "Employee",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(3154),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(7315));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Employee",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("6aa92f19-fe0d-4e46-b349-e2e7eacc2887"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("8094d7d3-20e6-4604-88a1-75d7b0a96981"));

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Communication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(5745),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(9522));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(5471),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Communication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("cb36a9c3-aadc-4671-8fde-bf07d61a2f58"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("3d5d8c3a-51e5-41d8-87df-7550757a64de"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Accomplishment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(4857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(8702));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Accomplishment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(4457),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(8374));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Accomplishment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("07e34d76-829c-478e-a415-2194893468c6"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("664e7099-0007-41ca-bc54-02f6375c98b9"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Employee");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Skill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(2065),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Skill",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(1809),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(8202));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Skill",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("9f8d5f97-dbce-40c0-8981-c537fa4f53a6"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("7d3d7554-433a-4122-9b13-a05f57acabde"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Experience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(1282),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(7650));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Experience",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(7284));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Experience",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("0cd96cc4-deb0-4336-8e3b-943c69440d70"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("1776730a-be0d-44c7-b41d-5e5ee660fc9d"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(431),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(6701));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 680, DateTimeKind.Local).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(6412));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("37ef3607-5aa6-4dd8-8eb6-d18aa763f00d"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("73186bc0-9714-4dfd-aad5-cc1cdac206da"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(7805),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(3668));

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfficeAddress",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainTelephoneNumber",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainEmail",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EmploymentDate",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(7315),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(3154));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("8094d7d3-20e6-4604-88a1-75d7b0a96981"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("6aa92f19-fe0d-4e46-b349-e2e7eacc2887"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Communication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(9522),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(5745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Communication",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(9254),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Communication",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("3d5d8c3a-51e5-41d8-87df-7550757a64de"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("cb36a9c3-aadc-4671-8fde-bf07d61a2f58"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Accomplishment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(8702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(4857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Accomplishment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 7, 20, 7, 20, 679, DateTimeKind.Local).AddTicks(8374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 11, 13, 22, 31, 17, 544, DateTimeKind.Local).AddTicks(4457));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Accomplishment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("664e7099-0007-41ca-bc54-02f6375c98b9"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("07e34d76-829c-478e-a415-2194893468c6"));
        }
    }
}
