using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class CustomizeUpdateAdvanceMoneyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "BookingCharge",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "BookingCharge",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "BookingCharge",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceMoney_BookingId",
                table: "AdvanceMoney",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvanceMoney_Booking_BookingId",
                table: "AdvanceMoney",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");
         }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvanceMoney_Booking_BookingId",
                table: "AdvanceMoney");

            migrationBuilder.DropIndex(
                name: "IX_AdvanceMoney_BookingId",
                table: "AdvanceMoney");

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "BookingCharge",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "BookingCharge",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "BookingCharge",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
