using Microsoft.Extensions.DependencyInjection;
using Project.Common.Domain.Tests;
using Project.Common.Infrastructure.SQLDB.Tests;

namespace Project.Common.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions {
    public static void AddCommonSQLDB(this IServiceCollection services) {
        services.AddScoped<ITestRepository, TestRepository>();
    }
}