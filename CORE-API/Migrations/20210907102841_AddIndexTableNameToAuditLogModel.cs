using Microsoft.EntityFrameworkCore.Migrations;

namespace CORE_API.Migrations
{
    public partial class AddIndexTableNameToAuditLogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "AuditLog",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TableName",
                table: "AuditLog",
                column: "TableName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditLog_TableName",
                table: "AuditLog");

            migrationBuilder.AlterColumn<string>(
                name: "TableName",
                table: "AuditLog",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);
        }
    }
}
