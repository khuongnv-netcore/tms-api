using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class CustomizePricingMasterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "PricingMaster");

            migrationBuilder.RenameColumn(
                name: "PricingMasterId",
                table: "PricingForCustomerDetail",
                newName: "PricingMasterDetailId");

            migrationBuilder.AddColumn<int>(
                name: "FeeType",
                table: "PricingMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PricingMasterDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PricingMasterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingMasterDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingMasterDetail_Container_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Container",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PricingMasterDetail_Location_FromLocationId",
                        column: x => x.FromLocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PricingMasterDetail_Location_ToLocationId",
                        column: x => x.ToLocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PricingMasterDetail_PricingMaster_PricingMasterId",
                        column: x => x.PricingMasterId,
                        principalTable: "PricingMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingMasterDetail_ContainerId",
                table: "PricingMasterDetail",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingMasterDetail_Created",
                table: "PricingMasterDetail",
                column: "Created");

            migrationBuilder.CreateIndex(
                name: "IX_PricingMasterDetail_FromLocationId",
                table: "PricingMasterDetail",
                column: "FromLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingMasterDetail_Modified",
                table: "PricingMasterDetail",
                column: "Modified");

            migrationBuilder.CreateIndex(
                name: "IX_PricingMasterDetail_PricingMasterId",
                table: "PricingMasterDetail",
                column: "PricingMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingMasterDetail_ToLocationId",
                table: "PricingMasterDetail",
                column: "ToLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingMasterDetail");

            migrationBuilder.DropColumn(
                name: "FeeType",
                table: "PricingMaster");

            migrationBuilder.RenameColumn(
                name: "PricingMasterDetailId",
                table: "PricingForCustomerDetail",
                newName: "PricingMasterId");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "PricingMaster",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
