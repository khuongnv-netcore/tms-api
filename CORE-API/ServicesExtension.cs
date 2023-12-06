using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CORE_API.CORE.Contexts;
using Microsoft.AspNetCore.Http;
using CORE_API.CORE.Services;
using CORE_API.CORE.Services.Abstract;
using CORE_API.CORE.Repositories;
using CORE_API.CORE.Repositories.Abstract;
using CORE_API.CORE.Models.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Security.Claims;
using CORE_API.CORE.Models.Entities;
using System.Collections.Generic;
using CORE_API.CORE.Helpers.Abstract;
using CORE_API.CORE.Helpers;
using CORE_API.CORE.Helpers.Attributes;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using CORE_API.CORE.Exceptions;

namespace CORE_API
{
    public static class ServicesExtension
    {
        private const string Custom = nameof(Custom);
        private static CoreConfigurationOptions _configurationOptions { get; set; }

        public static void ConfigureBaseOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CoreConfigurationOptions>(configuration.GetSection(typeof(CoreConfigurationOptions).Name));
            setConfigurationOptions(services);
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = _configurationOptions.Name, Version = $"v{ _configurationOptions.Version }" });

                c.AddSecurityDefinition("Auth0", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(_configurationOptions.AuthenticationOptions.getAuthorizationWithAudienceUrl(), UriKind.Absolute),
                            TokenUrl = new Uri(_configurationOptions.AuthenticationOptions.TokenUrl, UriKind.Absolute)
                        }
                    }
                });

                c.OperationFilter<ApplySwaggerSummaryAttribute>();

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Auth0" }
                        },
                        new string[]{ }
                    }
                });
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = _configurationOptions.AuthenticationOptions.Domain;
                options.Audience = _configurationOptions.AuthenticationOptions.Audience;

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = OnTokenValidated
                };
            }).AddJwtBearer(Custom, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(_configurationOptions.CustomAuthenticationOptions.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = OnCustomTokenValidated
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, Custom)
                    .Build();
            });
        }
        private static async Task OnTokenValidated(TokenValidatedContext context)
        {
            string sub = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userService = context.HttpContext.RequestServices.GetRequiredService<IGenericEntityService<User>>();

            //ParseToken
            string tokenEmailAddress = context.Principal.FindFirst(_configurationOptions.AuthenticationOptions.ClaimsOptions.EmailAddress).Value;
            string tokenEmailVerified = context.Principal.FindFirst(_configurationOptions.AuthenticationOptions.ClaimsOptions.EmailVerified).Value;

            //Find user by Auth0Id
            var user = userService.FindOne(x => x.AuthId == sub);

            //If Auth0Id not tied to user
            if (user == null)
            {
                //Check if user exists by email
                user = userService.FindOne(x => x.EmailAddress.Equals(tokenEmailAddress));

                if (user == null)
                {
                    //User does not exits. Create them.
                    user = new User
                    {
                        AuthId = sub,
                        EmailAddress = tokenEmailAddress,
                        EmailVerified = tokenEmailVerified
                    };
                    await userService.AddAsync(user);
                }
                else
                {
                    //User exists, attach Auth0Id and update properties
                    user.AuthId = sub;
                    user.EmailAddress = tokenEmailAddress;
                    user.EmailVerified = tokenEmailVerified;

                    await userService.UpdateAsync(user);
                }

            }
            else
            {
                //Auth0Id attached to user, update email and verified status
                if (user.EmailAddress != tokenEmailAddress || user.EmailVerified != tokenEmailVerified)
                {
                    user.EmailAddress = tokenEmailAddress;
                    user.EmailVerified = tokenEmailVerified;

                    await userService.UpdateAsync(user);
                }
            }
            if (user.UserRoles.Count == 0)
            {
                return;
            }

            var claims = new List<Claim>();

            foreach (var userRole in user.UserRoles)
            {
                var claim = new Claim(ClaimTypes.Role.Trim(), userRole.Role.DisplayName.Trim());
                claims.Add(claim);
            }

            Claim primarySID = new Claim(ClaimTypes.PrimarySid, user.Id.ToString());
            claims.Add(primarySID);

            if (!string.IsNullOrWhiteSpace(user.StripeCustomerId))
            {
                Claim stripeCustomerId = new Claim("StripeCustomerId", user.StripeCustomerId);
                claims.Add(stripeCustomerId);
            }

            if (!string.IsNullOrWhiteSpace(user.SubscriptionProduct))
            {
                Claim subscriptionProduct = new Claim("SubscriptionProduct", user.SubscriptionProduct);
                claims.Add(subscriptionProduct);
            }

            Claim subscriptionEndDate = new Claim("SubscriptionEndDate", user.SubscriptionEndDate.HasValue ? user.SubscriptionEndDate.ToString() : "");
            claims.Add(subscriptionEndDate);

            context.Principal.AddIdentity(new ClaimsIdentity(claims));

        }

        private static async Task OnCustomTokenValidated(TokenValidatedContext context)
        {
            var userService = context.HttpContext.RequestServices.GetRequiredService<IGenericEntityService<User>>();
            var authenticationService = context.HttpContext.RequestServices.GetRequiredService<IGenericEntityService<Authentication>>();
            //ParseToken
            string tokenEmailAddress = context.Principal.FindFirst(ClaimTypes.Email).Value;
            Guid tokenId = Guid.Parse(context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            //Check if user exists by email
            var user = userService.FindOne(x => x.EmailAddress.Equals(tokenEmailAddress));
            var token = authenticationService.FindOne(x => x.Id == tokenId);

            if (token == null)
            {
                throw new ApiNotAuthorizedException("Token not found.");
            }
            if (token.IsExpired == true || token.ExpiredDate < DateTime.Now)
            {
                throw new ApiNotAuthorizedException("Token had been Expired.");
            }
            if (user == null)
            {
                throw new ApiNotAuthorizedException("User not found for provided token.");
            }
            if (user.UserRoles == null)
            {
                return;
            }

            var claims = new List<Claim>();

            foreach (var userRole in user.UserRoles)
            {
                var claim = new Claim(ClaimTypes.Role.Trim(), userRole.Role.DisplayName.Trim());
                claims.Add(claim);
            }

            Claim primarySID = new Claim(ClaimTypes.PrimarySid, user.Id.ToString());
            claims.Add(primarySID);

            context.Principal.AddIdentity(new ClaimsIdentity(claims));
        }

        public static void ConfigureDatabaseConnections(this IServiceCollection services, IConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(_configurationOptions.ConnectionStrings.CoreConnection))
            {
                services.AddDbContext<CoreContext>(options =>
                    options.UseMySql(_configurationOptions.ConnectionStrings.CoreConnection,
                    ServerVersion.AutoDetect(_configurationOptions.ConnectionStrings.CoreConnection)
                    )
                );
            }
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        #region RegisterServices_Repositories
        public static void RegisterServices(this IServiceCollection services)
        {
            // Add HttpContextAccessor to make context available in classes other than controllers
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IGenericEntityService<>), typeof(GenericEntityService<>));
            //services.AddScoped(typeof(ControllerHelper), typeof(ControllerHelper));

            // Add services defined in project (Name ends with "Service")
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(c => c.Where(x => x.Name.EndsWith("Service")), publicOnly: true)
                .AsMatchingInterface((service, filter) =>
                    filter.Where(implementation => implementation.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
                .WithTransientLifetime());

        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            // Add HttpContextAccessor to make context available in classes other than controllers
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IGenericEntityRepository<>), typeof(GenericEntityRepository<>));

            // Add services defined in project (Name ends with "Service")
            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses(c => c.Where(x => x.Name.EndsWith("Repository")), publicOnly: true)
                .AsMatchingInterface((service, filter) =>
                    filter.Where(implementation => implementation.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
                .WithTransientLifetime());
        }

        public static void RegisterCustomHelpers(this IServiceCollection services)
        {
            services.AddScoped(typeof(IControllerHelper), typeof(ControllerHelper));
        }
        #endregion

        private static void setConfigurationOptions(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            _configurationOptions = serviceProvider.GetRequiredService<IOptions<CoreConfigurationOptions>>().Value;
        }
    }
}

