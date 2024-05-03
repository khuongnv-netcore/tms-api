using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class addColumnToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerCode",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerCode",
                table: "Customer");
        }
    }
}
