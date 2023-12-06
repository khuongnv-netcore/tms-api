using CORE_API.CORE.Contexts;
using CORE_API.Contexts.DataSeeders;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CORE_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<CoreContext>())
            {
                context.Database.Migrate();
                var dataSeeder = new DataSeeder(context);
                dataSeeder.SeedData().Wait();
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(
                options =>
                {
                    options.Limits.MaxRequestBodySize = 1073741824; //1GB
                }
                )
            .UseSentry()
            .UseStartup<Startup>()
            .Build();
    }
}
