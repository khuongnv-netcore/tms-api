using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class AdvanceMoneyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractNo",
                table: "PricingForCustomer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdvanceMoney",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceMoney", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AutoNumber",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AutoNumberType = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegExp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoNumber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvanceMoneyDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdvanceMoneyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceMoneyDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvanceMoneyDocument_AdvanceMoney_AdvanceMoneyId",
                        column: x => x.AdvanceMoneyId,
                        principalTable: "AdvanceMoney",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingForCustomer_ContractNo",
                table: "PricingForCustomer",
                column: "ContractNo",
                unique: true,
                filter: "[ContractNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceMoney_Created",
                table: "AdvanceMoney",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceMoney_Modified",
                table: "AdvanceMoney",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceMoneyDocument_AdvanceMoneyId",
                table: "AdvanceMoneyDocument",
                column: "AdvanceMoneyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceMoneyDocument_Created",
                table: "AdvanceMoneyDocument",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceMoneyDocument_Modified",
                table: "AdvanceMoneyDocument",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_AutoNumber_AutoNumberType",
                table: "AutoNumber",
                column: "AutoNumberType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutoNumber_Created",
                table: "AutoNumber",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AutoNumber_Modified",
                table: "AutoNumber",
                column: "Modified");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceMoneyDocument");

            migrationBuilder.DropTable(
                name: "AutoNumber");

            migrationBuilder.DropTable(
                name: "AdvanceMoney");

            migrationBuilder.DropIndex(
                name: "IX_PricingForCustomer_ContractNo",
                table: "PricingForCustomer");

            migrationBuilder.DropColumn(
                name: "ContractNo",
                table: "PricingForCustomer");
        }
    }
}
