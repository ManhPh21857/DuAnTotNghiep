using Microsoft.Extensions.DependencyInjection;
using Project.Common.Domain.Garbage;
using Project.Common.Infrastructure.SQLDB.Garbage;

namespace Project.Common.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddCommonSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IGarbageRepository, GarbageRepository>();
    }
}