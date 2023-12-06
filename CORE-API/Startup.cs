using CORE_API.CORE.Exceptions;
using AutoMapper;
using CORE_API.CORE.Helpers;
using CORE_API.CORE.Models.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SumoLogic.Logging.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authorization;
using CORE_API.CORE.Authorization;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SimpleEmailV2;
using CORE_API.CORE.Services;
using CORE_API.CORE.Services.Background;

namespace CORE_API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureBaseOptions(_configuration);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.RegisterCustomHelpers();

            services.RegisterServices();

            services.RegisterRepositories();

            services.ConfigureAuthentication();

            services.ConfigureSwaggerGen();

            services.ConfigureDatabaseConnections(Configuration);

            services.ConfigureCors();

            services.AddAutoMapper(typeof(Startup));

            IdentityModelEventSource.ShowPII = true;

            services.AddSingleton<IAuthorizationPolicyProvider, SubscriptionPolicyProvider>();

            services.AddSingleton<IAuthorizationHandler, SubscriptionAuthorizationHandler>();

            services.AddHttpContextAccessor();

            //Register AWS

            services.AddDefaultAWSOptions(GetAWSOptions());

            services.AddAWSService<IAmazonSimpleEmailServiceV2>();

            //Register Background Services

            //services.AddHostedService<CoreBackgroundService>();
            //services.AddHostedService<CronJobBackgroundService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var configOptions = app.ApplicationServices.GetRequiredService<IOptions<CoreConfigurationOptions>>().Value;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(
                    c =>
                    {
                        c.PreSerializeFilters.Add((swagger, httpReq) =>
                        {
                            swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}" } };
                        });
                    }
                    );
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(configOptions.SwaggerOptions.SpecUrl, "CORE_API");
                    c.OAuthClientId(configOptions.SwaggerOptions.ClientId);
                    c.OAuthClientSecret(configOptions.SwaggerOptions.ClientSecret);
                    c.OAuthUsePkce();
                    c.EnableDeepLinking();
                });

                loggerFactory.AddFile("LocalLogs/api-{Date}.txt");
            }
            else
            {
                if (!string.IsNullOrEmpty(configOptions.LoggingOptions.SumoLogicUri))
                {
                    loggerFactory.AddSumoLogic(configOptions.LoggingOptions.SumoLogicUri);
                }
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private AWSOptions GetAWSOptions()

        {

            var awsOptions = _configuration.GetAWSOptions();


            //By default, the credentials informations are loaded from the profile indicated in the appsettings.json (AWS:Profile). If none is provided, it will look for a profile called default.


            //You may want to use an alternative authentication strategy


            //To use AccessKey and SecretKey

            //awsOption.Credentials = new BasicAWSCredentials(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"])


            //To use environment variables

            //Set AWS_ACCESS_KEY_ID & AWS_SECRET_ACCESS_KEY & AWS_REGION in your environment.

            //awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();


            //More info here https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-netcore.html


            return awsOptions;

        }
    }
}
