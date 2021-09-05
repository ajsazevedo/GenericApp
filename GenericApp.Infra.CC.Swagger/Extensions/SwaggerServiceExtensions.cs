using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace GenericApp.Infra.CC.Swagger.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Generic App API",
                    Version = "v0.10.00",
                    Description = "Endpoints for apps",
                    Contact = new OpenApiContact
                    {
                        Name = "Generic App",
                        Email = ""
                    }
                });

                var security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                };

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Add 'Bearer <token>'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(security);

                c.OperationFilter<CultureAwareOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            var option = new RewriteOptions();
            option.AddRedirect("^$", "api/generic/swagger");
            app.UseRewriter(option);

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    var scheme = httpReq.Host.Host.StartsWith("localhost", StringComparison.OrdinalIgnoreCase) ? "http" : "https";
                    swagger.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = $"{scheme}://{httpReq.Host}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Generic App v0.10.00");
                c.RoutePrefix = "api/generic/swagger";
            });

            return app;
        }
    }
}
