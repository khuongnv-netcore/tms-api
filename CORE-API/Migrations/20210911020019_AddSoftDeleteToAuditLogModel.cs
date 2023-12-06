using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CORE_API.Migrations
{
    public partial class AddSoftDeleteToAuditLogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "AuditLog",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AuditLog");
        }
    }
}
