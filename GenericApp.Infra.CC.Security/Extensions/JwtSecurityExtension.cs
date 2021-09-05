using GenericApp.Infra.Common.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace GenericApp.Infra.CC.Security.Extensions
{
    public static class JwtSecurityExtension
    {
        public static IServiceCollection AddJwtSecurity(this IServiceCollection services, IConfigurationSection tokenConfiguration)
        {
            var tokenConfigurations = new TokenConfigurations(tokenConfiguration);
            services.AddSingleton(tokenConfigurations);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = tokenConfigurations.Audience,
                ValidIssuer = tokenConfigurations.Issuer,
                IssuerSigningKey = tokenConfigurations.Key,
                ClockSkew = TimeSpan.Zero,
                LifetimeValidator = LocalLifetimeValidator,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.TokenValidationParameters = tokenValidationParameters;
                bearerOptions.SaveToken = true;
                bearerOptions.RequireHttpsMetadata = true;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
                auth.AddPolicy(Role.Administrator.ToString(), p => p.RequireRole(Role.Administrator.ToString()));
                auth.AddPolicy(Role.Collaborator.ToString(), p => p.RequireRole(Role.Collaborator.ToString(), Role.Administrator.ToString()));
            });

            return services;
        }

        public static bool LocalLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return notBefore <= DateTime.UtcNow && expires >= DateTime.UtcNow;
        }
    }
}
