using Microsoft.OpenApi.Models;

namespace Sponsorship.Api.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerDocumentation(
       this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Sponsorship API",
                        Version = "v1",
                        Description = "Sponsorship Workflow API"
                    });

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",

                        Type = SecuritySchemeType.Http,

                        Scheme = "bearer",

                        BearerFormat = "JWT",

                        In = ParameterLocation.Header,

                        Description =
                            "Enter JWT token"
                    });

                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference =
                                new Microsoft.OpenApi.Models.OpenApiReference
                                {
                                    Type =
                                        ReferenceType.SecurityScheme,

                                    Id = "Bearer"
                                }
                        },

                        new List<string>()
                    }
                    });
            });
            return services;
        }
    }
}
