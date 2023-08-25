using Microsoft.Extensions.DependencyInjection;
using Project.Core.Infrastructure.SQLDB;

namespace Project.Core.Infrastructure.WebAPI;

public static class ServiceCollectionExtension {
    public static void AddCoreWebAPI(this IServiceCollection services) {
        services.AddCoreSQLDB();
    }
}