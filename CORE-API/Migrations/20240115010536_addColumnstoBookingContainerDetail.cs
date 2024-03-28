using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class addColumnstoBookingContainerDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "BookingContainerDetailId",
                table: "Schedule",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "BookingContainerDetail",
                type: "uniqueidentifier",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingContainerDetail_Schedule_ScheduleId1",
                table: "BookingContainerDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookingContainerDetail_ScheduleId1",
                table: "BookingContainerDetail");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "BookingContainerDetail");

            migrationBuilder.DropColumn(
                name: "ScheduleId1",
                table: "BookingContainerDetail");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingContainerDetailId",
                table: "Schedule",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
