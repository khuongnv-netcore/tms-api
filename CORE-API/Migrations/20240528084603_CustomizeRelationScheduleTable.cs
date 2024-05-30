using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class CustomizeRelationScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingContainerDetail_Schedule_ScheduleId1",
                table: "BookingContainerDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingContainerDetail_ScheduleId1",
                table: "BookingContainerDetail");

            migrationBuilder.DropColumn(
                name: "ScheduleId1",
                table: "BookingContainerDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_BookingContainerDetailId",
                table: "Schedule",
                column: "BookingContainerDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PricingForCustomerDetail_Created",
                table: "PricingForCustomerDetail",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_PricingForCustomerDetail_Modified",
                table: "PricingForCustomerDetail",
                column: "Modified");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_BookingContainerDetail_BookingContainerDetailId",
                table: "Schedule",
                column: "BookingContainerDetailId",
                principalTable: "BookingContainerDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_BookingContainerDetail_BookingContainerDetailId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_BookingContainerDetailId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_PricingForCustomerDetail_Created",
                table: "PricingForCustomerDetail");

            migrationBuilder.DropIndex(
                name: "IX_PricingForCustomerDetail_Modified",
                table: "PricingForCustomerDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId1",
                table: "BookingContainerDetail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainerDetail_ScheduleId1",
                table: "BookingContainerDetail",
                column: "ScheduleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingContainerDetail_Schedule_ScheduleId1",
                table: "BookingContainerDetail",
                column: "ScheduleId1",
                principalTable: "Schedule",
                principalColumn: "Id");
        }
    }
}
