using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviorClientCascadeAdvanceMoneyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvanceMoneyDocument_AdvanceMoney_AdvanceMoneyId",
                table: "AdvanceMoneyDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvanceMoneyDocument_AdvanceMoney_AdvanceMoneyId",
                table: "AdvanceMoneyDocument",
                column: "AdvanceMoneyId",
                principalTable: "AdvanceMoney",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvanceMoneyDocument_AdvanceMoney_AdvanceMoneyId",
                table: "AdvanceMoneyDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvanceMoneyDocument_AdvanceMoney_AdvanceMoneyId",
                table: "AdvanceMoneyDocument",
                column: "AdvanceMoneyId",
                principalTable: "AdvanceMoney",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
