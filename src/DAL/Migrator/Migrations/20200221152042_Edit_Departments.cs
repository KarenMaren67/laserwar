using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrator.Migrations
{
    public partial class Edit_Departments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentDepartment",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Departments",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Employees",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "ParentDepartmentId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasEmployees",
                table: "Departments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentDepartmentId",
                table: "Departments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentDepartmentId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HasEmployees",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ParentDepartmentId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "ParentDepartment",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int[]>(
                name: "Departments",
                table: "Departments",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "Employees",
                table: "Departments",
                type: "integer[]",
                nullable: true);
        }
    }
}
