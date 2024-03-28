using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributedLock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributedLock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StripeCustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SubscriptionProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    KeyValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLog_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Authentication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExpired = table.Column<bool>(type: "bit", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authentication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingType = table.Column<int>(type: "int", nullable: false),
                    ShipperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ForwarderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VirtualBookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VirtualBookingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleStatus = table.Column<int>(type: "int", nullable: false),
                    PickupAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TVVD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POR1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POR2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POD1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POD2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DEL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DEL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RDTerm1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RDTerm2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BLNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SI = table.Column<bool>(type: "bit", nullable: false),
                    BRD = table.Column<bool>(type: "bit", nullable: false),
                    PickUpCy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullReturnCy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BKGContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BKGContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BKGContactTel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fh = table.Column<bool>(type: "bit", nullable: false),
                    CMTD1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CMTD2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    LOFC1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LOFC2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaillingDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickUpDT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ETBDT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_User_ApprovedUserId",
                        column: x => x.ApprovedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Booking_User_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookingContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingContainerDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContainerNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PickupPlan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActTd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryPlan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InprocessDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefuseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActTa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContainerTruckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerTruckCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DriverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HourNumberAlarm = table.Column<int>(type: "int", nullable: false),
                    ScheduleStatus = table.Column<int>(type: "int", nullable: false),
                    StartLocation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PickupAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EndLocation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TransportCost = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    Vgm = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Schedule_User_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationUserRole = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrganization_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrganization_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingContainer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vol = table.Column<int>(type: "int", nullable: false),
                    EQSub = table.Column<double>(type: "float", nullable: false),
                    SOC = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingContainer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingContainer_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingContainer_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingContainer_User_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookingContainerDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SealNo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SealNo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Package = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    VGM = table.Column<double>(type: "float", nullable: false),
                    Measure = table.Column<double>(type: "float", nullable: false),
                    ST = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RF = table.Column<bool>(type: "bit", nullable: false),
                    Scheduled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingContainerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingContainerDetail_BookingContainer_BookingContainerId",
                        column: x => x.BookingContainerId,
                        principalTable: "BookingContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingContainerDetail_User_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookingContainerDetail_User_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_Created",
                table: "AuditLog",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_Modified",
                table: "AuditLog",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_TableName",
                table: "AuditLog",
                column: "TableName");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_UserId",
                table: "AuditLog",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_Created",
                table: "Authentication",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_Modified",
                table: "Authentication",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_UserId",
                table: "Authentication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ApprovedUserId",
                table: "Booking",
                column: "ApprovedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Created",
                table: "Booking",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CreatedUserId",
                table: "Booking",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Modified",
                table: "Booking",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ModifiedUserId",
                table: "Booking",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainer_BookingId",
                table: "BookingContainer",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainer_Created",
                table: "BookingContainer",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainer_CreatedUserId",
                table: "BookingContainer",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainer_Modified",
                table: "BookingContainer",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainer_ModifiedUserId",
                table: "BookingContainer",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainerDetail_BookingContainerId",
                table: "BookingContainerDetail",
                column: "BookingContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainerDetail_Created",
                table: "BookingContainerDetail",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainerDetail_CreatedUserId",
                table: "BookingContainerDetail",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainerDetail_Modified",
                table: "BookingContainerDetail",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_BookingContainerDetail_ModifiedUserId",
                table: "BookingContainerDetail",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DistributedLock_LockName",
                table: "DistributedLock",
                column: "LockName",
                unique: true,
                filter: "[LockName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Created",
                table: "Role",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Modified",
                table: "Role",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Created",
                table: "Schedule",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_CreatedUserId",
                table: "Schedule",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_Modified",
                table: "Schedule",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_ModifiedUserId",
                table: "Schedule",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_AuthId",
                table: "User",
                column: "AuthId",
                unique: true,
                filter: "[AuthId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Created",
                table: "User",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmailAddress",
                table: "User",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Modified",
                table: "User",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_User_StripeCustomerId",
                table: "User",
                column: "StripeCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_Created",
                table: "UserOrganization",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_Modified",
                table: "UserOrganization",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_OrganizationId",
                table: "UserOrganization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganization_UserId",
                table: "UserOrganization",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Created",
                table: "UserRole",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_Modified",
                table: "UserRole",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "Authentication");

            migrationBuilder.DropTable(
                name: "BookingContainerDetail");

            migrationBuilder.DropTable(
                name: "DistributedLock");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "UserOrganization");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "BookingContainer");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
