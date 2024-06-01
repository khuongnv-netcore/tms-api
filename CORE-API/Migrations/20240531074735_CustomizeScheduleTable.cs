using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CORE_API.Migrations
{
    /// <inheritdoc />
    public partial class CustomizeScheduleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IX_Schedule_BookingContainerDetailId ON Schedule;");
            migrationBuilder.Sql("CREATE INDEX IX_Schedule_BookingContainerDetailId ON Schedule (BookingContainerDetailId) WHERE DeletedAt is null;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IX_Schedule_BookingContainerDetailId ON Schedule;");
            migrationBuilder.Sql("CREATE INDEX IX_Schedule_BookingContainerDetailId ON Schedule (BookingContainerDetailId);");
        }
    }
}
