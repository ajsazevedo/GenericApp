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
using GenericApp.Services.Base;

namespace GenericApp.Infra.CC.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void StartupServices(this IServiceCollection services)
        {
            var startupParameters = DependencyInjectionStartup.SetParameters();

            services.AddDataAccessServices(startupParameters.ConnectionString);
            services.AddCommonServices();
            //services.AddSingletonServices(startupParameters);
            //services.AddTransientServices();
            services.AddScopedServices();
            //services.AddSingletonRepositories();
            services.AddScopedRepositories();
            //services.AddTransientRepositories();
            //services.AddWebServices(startupParameters.TokenConfiguration);
        }

        public static void AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<GenericAppContext>>();
            services.AddDbContext<GenericAppContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("GenericApp.Infra.Data.Migrations")))
                .AddScoped<GenericAppContext, GenericAppContext>();
        }
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }
        public static void UseAllOfType<T>(this IServiceCollection serviceCollection, Assembly[] assemblies, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.IsClass && x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                serviceCollection.Add(new ServiceDescriptor(type, type, lifetime));
        }

        public static void AddScopedRepositories(this IServiceCollection services)
        {
            services.UseAllOfType<IRepository>(new[] { typeof(Repository).Assembly });
        }

        public static void AddScopedServices(this IServiceCollection services)
        {
            services.UseAllOfType<IBaseService>(new[] { typeof(BaseService).Assembly });
        }
    }
}
