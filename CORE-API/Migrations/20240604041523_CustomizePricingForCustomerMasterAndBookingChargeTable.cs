using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class CustomizePricingForCustomerMasterAndBookingChargeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "PricingForCustomerDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FromLocationId",
                table: "PricingForCustomerDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ToLocationId",
                table: "PricingForCustomerDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "BookingCharge",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FromLocationId",
                table: "BookingCharge",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ToLocationId",
                table: "BookingCharge",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "PricingForCustomerDetail");

            migrationBuilder.DropColumn(
                name: "FromLocationId",
                table: "PricingForCustomerDetail");

            migrationBuilder.DropColumn(
                name: "ToLocationId",
                table: "PricingForCustomerDetail");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "BookingCharge");

            migrationBuilder.DropColumn(
                name: "FromLocationId",
                table: "BookingCharge");

            migrationBuilder.DropColumn(
                name: "ToLocationId",
                table: "BookingCharge");
        }
    }
}
