using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingForCustomerDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "PricingForCustomer");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PricingForCustomer");

            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "PricingForCustomer");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "PricingForCustomer");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "PricingForCustomer");

            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "PricingForCustomer",
                newName: "ToDatePeriod");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "PricingForCustomer",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "PricingForCustomer",
                newName: "FromDatePeriod");

            migrationBuilder.CreateTable(
                name: "PricingForCustomerDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PricingMasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceForSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PricingForCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingForCustomerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingForCustomerDetail_PricingForCustomer_PricingForCustomerId",
                        column: x => x.PricingForCustomerId,
                        principalTable: "PricingForCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingForCustomerDetail_PricingForCustomerId",
                table: "PricingForCustomerDetail",
                column: "PricingForCustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingForCustomerDetail");

            migrationBuilder.RenameColumn(
                name: "ToDatePeriod",
                table: "PricingForCustomer",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "PricingForCustomer",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "FromDatePeriod",
                table: "PricingForCustomer",
                newName: "FromDate");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "PricingForCustomer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "PricingForCustomer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "PricingForCustomer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "PricingForCustomer",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "PricingForCustomer",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
