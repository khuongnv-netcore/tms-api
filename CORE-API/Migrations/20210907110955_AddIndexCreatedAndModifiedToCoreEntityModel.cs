using Microsoft.EntityFrameworkCore.Migrations;

namespace CORE_API.Migrations
{
    public partial class AddIndexCreatedAndModifiedToCoreEntityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Created",
                table: "UserRole",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Modified",
                table: "UserRole",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_User_Created",
                table: "User",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_User_Modified",
                table: "User",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Created",
                table: "Role",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Modified",
                table: "Role",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_Created",
                table: "Authentication",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_Modified",
                table: "Authentication",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_Created",
                table: "AuditLog",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_Modified",
                table: "AuditLog",
                column: "Modified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRole_Created",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_Modified",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_User_Created",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Modified",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Role_Created",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_Modified",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Authentication_Created",
                table: "Authentication");

            migrationBuilder.DropIndex(
                name: "IX_Authentication_Modified",
                table: "Authentication");

            migrationBuilder.DropIndex(
                name: "IX_AuditLog_Created",
                table: "AuditLog");

            migrationBuilder.DropIndex(
                name: "IX_AuditLog_Modified",
                table: "AuditLog");
        }
    }
}
