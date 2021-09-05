using GenericApp.Infra.Data;
using GenericApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Linq;
using System.Reflection;
using GenericApp.Domain.Interfaces.Repositories.Base;
using GenericApp.Infra.Data.Repositories.Base;
using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Application.Services.Base;
using GenericApp.Infra.CC.Logging.Serilog;
using Microsoft.Extensions.Configuration;
using GenericApp.Infra.Common.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using GenericApp.Infra.CC.Security.Extensions;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Infra.CC.Interfaces;
using GenericApp.Domain.Interfaces.Services;
using GenericApp.Application.Services;
using System;
using GenericApp.Infra.CC.Localization.Resources;
using System.Collections.Generic;
using GenericApp.Infra.CC.Swagger.Extensions;
using GenericApp.Infra.CC.Mapping.Extensions;

namespace GenericApp.Infra.CC.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void StartupServices(this IServiceCollection services)
        {
            var startupParameters = DependencyInjectionStartup.SetParameters();

            services.AddSingleton(startupParameters.EmailConfiguration.Get<EmailSettings>());
            services.AddCommonServices(startupParameters.GeneralSettings);
            services.AddWebServices();
            services.AddJwtSecurity(startupParameters.TokenConfiguration);
            services.AddDataAccessServices(startupParameters.ConnectionString);
            services.AddScopedRepositories();
            services.AddScopedServices();
            services.AddSwaggerDocumentation();
            services.AddMappingConfiguration();
        }

        private static void AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
            LoggerManager.ConfigureSerilog(configuration);
            services.AddSingleton(Log.Logger);
            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("pt-BR"),
                    new CultureInfo("es"),
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        private static void AddWebServices(this IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
        }

        private static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IApplicationManager, ApplicationManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork<IGenericAppContext>>();
            services.AddDbContext<GenericAppContext>(options => options
            .UseSqlServer(connectionString, b => b.MigrationsAssembly("GenericApp.Infra.Data.Migrations")))
                .AddScoped<IGenericAppContext, GenericAppContext>();
        }

        private static void AddScopedRepositories(this IServiceCollection services)
        {
            services.UseAllOfType(new[] { typeof(Repository).Assembly, typeof(IRepository).Assembly }, "Repository");
        }

        private static void AddScopedServices(this IServiceCollection services)
        {
            services.UseAllOfType(new[] { typeof(BaseService).Assembly, typeof(IBaseService).Assembly }, "Service");
            services.AddSingleton<ITokenManagerService, TokenManagerService>();
        }

        private static void UseAllOfType(this IServiceCollection services, Assembly[] assemblies, string serviceType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.AddDependenciesByNamingConvention(
                assemblies,
                x => x.Name.EndsWith(serviceType),
                lifetime
            );
        }

        private static IServiceCollection AddDependenciesByNamingConvention(this IServiceCollection services, Assembly[] assemblies, Func<Type, bool> predicate, ServiceLifetime lifetime)
        {
            var implementations = new List<Type>();
            var interfaces = new List<Type>();

            foreach (var assembly in assemblies)
            {
                implementations.AddRange(assembly.ExportedTypes
                    .Where(x => !x.IsInterface && predicate(x)));
                interfaces.AddRange(assembly.ExportedTypes
                    .Where(x => x.IsInterface && predicate(x)));
            }

            foreach (var @interface in interfaces)
            {
                var implementation = implementations
                    .FirstOrDefault(x => @interface.IsAssignableFrom(x)
                        && $"I{x.Name}" == @interface.Name && !x.IsAbstract);

                if (implementation == null)
                    throw new InvalidOperationException(string.Format(SharedResource.CouldNotFindAnImplementationForTheInterface, @interface));

                switch (lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(@interface, implementation);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(@interface, implementation);
                        break;
                    default:
                        services.AddTransient(@interface, implementation);
                        break;
                }
            }

            return services;
        }
    }
}
