using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sponsorship.Application.Abstractions;
using Sponsorship.Application.Abstractions.Authentication;
using Sponsorship.Application.Abstractions.Persistence;
using Sponsorship.Infrastructure.Authentication;
using Sponsorship.Infrastructure.Identity;
using Sponsorship.Infrastructure.Persistence;
using System.Text;

namespace Sponsorship.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration, string myAllowSpecificOrigins)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.ConfigureJWTToken(configuration);
            services.AddAuthorization();
            services.AddCorsPolicy(myAllowSpecificOrigins);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IIdentityService, IdentityService>();

            return services;
        }

        private static IServiceCollection ConfigureJWTToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(
                configuration.GetSection(JwtOptions.SectionName));

            var jwtOptions = configuration
                .GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>()!;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    jwtOptions.SecretKey))
                    };
            });

            services.AddScoped<IJwtTokenGenerator,
                JwtTokenGenerator>();

            return services;
        }

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services, string myAllowSpecificOrigins)
        {

            return services.AddCors(opt =>
            {
                opt.AddPolicy(myAllowSpecificOrigins, policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:4200");
                });
            });
        }
    }
}
