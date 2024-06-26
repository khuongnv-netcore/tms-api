using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class updateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_ApprovedUserId",
                table: "Booking");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDeliveryDate",
                table: "Schedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedPickupDate",
                table: "Schedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContractId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SellerId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DocumentDate",
                table: "AdvanceMoneyDocument",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KindOfFeeId",
                table: "AdvanceMoneyDocument",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "AdvanceMoneyDate",
                table: "AdvanceMoney",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GpsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KindOfFee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindOfFee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Open = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_Created",
                table: "Device",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Modified",
                table: "Device",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_DriverLocation_Created",
                table: "DriverLocation",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_DriverLocation_Modified",
                table: "DriverLocation",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfFee_Created",
                table: "KindOfFee",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfFee_FeeName",
                table: "KindOfFee",
                column: "FeeName",
                unique: true,
                filter: "[FeeName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KindOfFee_Modified",
                table: "KindOfFee",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Created",
                table: "Notification",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Modified",
                table: "Notification",
                column: "Modified");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Employee_ApprovedUserId",
                table: "Booking",
                column: "ApprovedUserId",
                principalTable: "Employee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Employee_ApprovedUserId",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "DriverLocation");

            migrationBuilder.DropTable(
                name: "KindOfFee");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropColumn(
                name: "CompletedDeliveryDate",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "CompletedPickupDate",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ContractId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DocumentDate",
                table: "AdvanceMoneyDocument");

            migrationBuilder.DropColumn(
                name: "KindOfFeeId",
                table: "AdvanceMoneyDocument");

            migrationBuilder.DropColumn(
                name: "AdvanceMoneyDate",
                table: "AdvanceMoney");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_ApprovedUserId",
                table: "Booking",
                column: "ApprovedUserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
