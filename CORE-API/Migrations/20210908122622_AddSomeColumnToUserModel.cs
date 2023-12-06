using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CORE_API.Migrations
{
    public partial class AddSomeColumnToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "User",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                table: "User",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionProduct",
                table: "User",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_StripeCustomerId",
                table: "User",
                column: "StripeCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_User_StripeCustomerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "SubscriptionProduct",
                table: "User");
        }
    }
}
