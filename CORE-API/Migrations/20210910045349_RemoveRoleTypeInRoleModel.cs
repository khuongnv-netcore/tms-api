using Microsoft.EntityFrameworkCore.Migrations;

namespace CORE_API.Migrations
{
    public partial class RemoveRoleTypeInRoleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
