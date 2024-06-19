using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviorAdvanceMoneyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingCharge_AdvanceMoneyDocument_AdvanceMoneyDocumentId",
                table: "BookingCharge");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BookingCharge_AdvanceMoneyDocument_AdvanceMoneyDocumentId",
                table: "BookingCharge",
                column: "AdvanceMoneyDocumentId",
                principalTable: "AdvanceMoneyDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
