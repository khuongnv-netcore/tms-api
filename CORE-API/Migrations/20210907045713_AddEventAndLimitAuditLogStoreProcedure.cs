using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace CORE_API.Migrations
{
    public partial class AddEventAndLimitAuditLogStoreProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd 00:00:00");
            var dayLimitAuditLog = GetDayLimitAuditLog();
            migrationBuilder.Sql(@"DROP Event IF EXISTS `EventDayLimitAuditLog`;");
            migrationBuilder.Sql(string.Format(@"CREATE EVENT `EventDayLimitAuditLog`
                ON SCHEDULE EVERY 1 DAY STARTS '{0}'
                DO BEGIN
                    DELETE FROM AuditLog WHERE DATE(Created) <= DATE_SUB(DATE(NOW()), INTERVAL {1} DAY); 
                END;
            ", currentDate, dayLimitAuditLog));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP Event IF EXISTS `EventDayLimitAuditLog`;");
        }

        private static string GetDayLimitAuditLog()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var dayLimitAuditLog = builder.Build().GetSection("CoreConfigurationOptions:DayLimitAuditLog").Value;

            return dayLimitAuditLog;
        }
    }
}
