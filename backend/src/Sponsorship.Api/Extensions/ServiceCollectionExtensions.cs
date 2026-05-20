using Asp.Versioning;

namespace Sponsorship.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.ConfigureVersioning();
            services.AddSwaggerDocumentation();
            return services;
        }
    }
}
