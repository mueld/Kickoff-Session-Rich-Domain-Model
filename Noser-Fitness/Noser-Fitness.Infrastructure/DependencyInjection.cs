using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noser_Fitness_Application.Abstractions;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Infrastructure.Interceptors;

namespace Noser_Fitness.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration)
        {
            services.AddDatabase(configuration).AddServices();
            return services;
        }

        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");
            ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));
            services.AddScoped<PublishDomainEventsInterceptor>();
            services.AddDbContext<NoserFitnessDbContext>(
                (sp, optionsBuilder) =>
                {
                    var interceptor = sp.GetRequiredService<PublishDomainEventsInterceptor>();
                    optionsBuilder.AddInterceptors(interceptor);
                    optionsBuilder.UseNpgsql(
                        connectionString,
                        npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName)
                    );
                }
            );
            services.AddScoped<INoserFitnessDbContext>(sp => sp.GetRequiredService<NoserFitnessDbContext>());
            return services;
        }

        private IServiceCollection AddServices()
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
