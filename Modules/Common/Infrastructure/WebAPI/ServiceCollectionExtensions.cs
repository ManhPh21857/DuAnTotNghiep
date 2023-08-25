using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Common.Domain;
using Project.Common.Infrastructure.SQLDB;
using System.Reflection;
using Project.Common.ApplicationService;

namespace Project.Common.Infrastructure.WebAPI;

public static class ServiceCollectionExtensions {
    public static void AddCommonWebAPI(this IServiceCollection services) {
        services.AddCommonDomain();
        services.AddCommonApplicationService();
        services.AddCommonSQLDB();
        services.AddCustomIdentity();
        services.AddValidator();
    }

    public static void AddCustomIdentity(this IServiceCollection services) {
    }

    public static void RegisterMapsterConfiguration(this IServiceCollection services) {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }

    private static void AddValidator(this IServiceCollection services) {
    }
}