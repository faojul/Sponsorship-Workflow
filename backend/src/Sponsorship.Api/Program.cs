using Sponsorship.Api.Extensions;
using Sponsorship.Application.Extensions;
using Sponsorship.Infrastructure.Extensions;
using Sponsorship.Infrastructure.Persistence;

namespace Sponsorship.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add User Secrets only when developing locally
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            // Base configuration (Loads appsettings.json for local development)
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Bind everything to the native builder configuration instance
            // This automatically pulls from appsettings.json locally AND environment variables on Render
            var config = builder.Configuration;

            // Add services to the container.
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            // Add services to the container.
            builder.Services.AddControllers();
            //// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplication();
            builder.Services.AddPresentation();
            builder.Services.AddInfrastructure(config, MyAllowSpecificOrigins);

            var app = builder.Build();

            app.UseCors(MyAllowSpecificOrigins);
            // Force HTTPS immediately for security
            app.UseHttpsRedirection();
            // Serve static files (if any)
            app.UseStaticFiles();

            ///if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();

                app.UseSwaggerUI();
            //}

            // Authenticate the request
            app.UseAuthentication();

            // Authorize the authenticated request
            app.UseAuthorization();

            // Run database migrations and seed data BEFORE mapping routes or running the app
            try
            {
                await ApplicationDbInitializer.InitialiseAsync(app.Services);
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during database migration or seeding.");
                throw; // Rethrow to stop the app if the DB state is invalid
            }

            // Route to controllers
            app.MapControllers();

            app.Run();
        }
    }
}
