using Domain.Repositories;
using Domain.Services;
using Infrastructure.Database;
using Infrastructure.Options;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class HostBuilderExtensions
{
    public static void ConfigureInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddJwtService();
        builder.Services.AddHashService();
        builder.Services.AddValidationService();
        builder.Services.AddRepositories();
        builder.ConfigureOptions();
    }

    private static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ConnectionStrings>(
            builder.Configuration.GetSection(ConnectionStrings.Section));
        builder.Services.Configure<JwtOptions>(
            builder.Configuration.GetSection(JwtOptions.Section));
    }

    private static void AddJwtService(this IServiceCollection services)
    {
        services.AddSingleton<IJwtService, JwtService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<MongoContext>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddHashService(this IServiceCollection services)
    {
        services.AddSingleton<IHashService, HashService>();
    }

    private static void AddValidationService(this IServiceCollection services)
    {
        services.AddSingleton<IValidationService, ValidationService>();
    }
}