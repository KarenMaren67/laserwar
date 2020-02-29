using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrator.Migrations
{
    public partial class Delete_parent_id_in_Dept : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentDepartment",
                table: "Departments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentDepartment",
                table: "Departments",
                type: "integer",
                nullable: true);
        }
    }
}
