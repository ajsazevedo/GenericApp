using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using GenericApp.Infra.CC.Swagger.Extensions;
using GenericApp.Infra.CC.Exceptions.Middleware;

namespace GenericApp.Infra.CC.IoC.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddCommonConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();
            app.UseRequestLocalization();
            ConfigurationBuilder(env);

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("Authorization"));
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapSwagger("/swagger/{documentName}/swagger.json", options =>
                {
                    options.PreSerializeFilters.Add((swagger, httpRequest) => { });
                });
            });

            app.UseSwaggerDocumentation();
        }

        private static void ConfigurationBuilder(IWebHostEnvironment env)
        {

            new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
