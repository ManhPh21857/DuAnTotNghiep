using Microsoft.Extensions.DependencyInjection;
using Project.Core.Infrastructure.SQLDB.Providers;

namespace Project.Core.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddCoreSQLDB(this IServiceCollection services)
    {
        services.AddProvider();
    }

    private static void AddProvider(this IServiceCollection services)
    {
        services.AddScoped<ConnectionProvider>();
    }
}