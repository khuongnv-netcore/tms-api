using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelateBookingChargeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookingCharge_AdvanceMoneyDocumentId",
                table: "BookingCharge",
                column: "AdvanceMoneyDocumentId",
                unique: false,
                filter: "[AdvanceMoneyDocumentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCharge_AdvanceMoneyDocument_AdvanceMoneyDocumentId",
                table: "BookingCharge",
                column: "AdvanceMoneyDocumentId",
                principalTable: "AdvanceMoneyDocument",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCharge_AdvanceMoneyDocument_AdvanceMoneyDocumentId",
                table: "BookingCharge");

            migrationBuilder.DropIndex(
                name: "IX_BookingCharge_AdvanceMoneyDocumentId",
                table: "BookingCharge");
        }
    }
}
