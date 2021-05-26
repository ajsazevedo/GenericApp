using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using GenericApp.Infra.CC.Exceptions.Extensions;

namespace GenericApp.Infra.CC.IoC.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void AddCommonConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureCustomExceptionMiddleware();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
