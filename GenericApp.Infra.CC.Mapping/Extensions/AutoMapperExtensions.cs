using AutoMapper.Extensions.ExpressionMapping;
using GenericApp.Infra.CC.Mapping.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace GenericApp.Infra.CC.Mapping.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddMappingConfiguration(this IServiceCollection services)
        {
            return services.AddAutoMapper(cfg =>
                { cfg.AddExpressionMapping(); cfg.AddProfile<MappingProfile>(); });
        }
    }
}
