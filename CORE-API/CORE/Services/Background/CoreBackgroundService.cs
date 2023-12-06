using CORE_API.CORE.Models.Configuration;
using CORE_API.CORE.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CORE_API.CORE.Services.Background
{
    public class CoreBackgroundService : BackgroundService
    {
        private readonly ILogService _logger;
        private readonly CoreConfigurationOptions _coreConfigurationOptions;

        protected readonly IServiceProvider ServiceProvider;

        public CoreBackgroundService(
            IServiceProvider serviceProvider,
            ILogService logger,
            IOptions<CoreConfigurationOptions> coreConfigurationOptions
            )
        {
            ServiceProvider = serviceProvider;
            _coreConfigurationOptions = coreConfigurationOptions.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //The queue service is registered as scope because we might want to have more than one backgroud queue service in the same app.
            //Since the BackgroundWorkerService is not a scoped service, it will throw an exception if we try to inject a scoped service
            //We muste then create a new scope and use is to instantiuate the required service
            //You may register IQueueService as a singleton and inject it in the constructor if you like. Your choice.
            using var scope = ServiceProvider.CreateScope();

            //this is an example of how to inject a service
            var _logger = scope.ServiceProvider.GetRequiredService<ILogService>();

            while (!stoppingToken.IsCancellationRequested)
            {
              _logger.LogInternalMessage("Background Service Ran At: " + DateTime.UtcNow.ToString());
              await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken);
            }
        }
    }
}
