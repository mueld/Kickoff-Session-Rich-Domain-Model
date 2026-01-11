using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noser_Fitness.Domain.Abstractions;
using Noser_Fitness.Infrastructure.Interceptors;

namespace Noser_Fitness.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("IndustrialScaleNetDb");
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
    }
}
