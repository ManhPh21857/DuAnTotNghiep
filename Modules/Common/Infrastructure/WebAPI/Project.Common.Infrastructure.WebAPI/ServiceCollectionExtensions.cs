using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Common.ApplicationService;
using Project.Common.Domain;
using Project.Common.Infrastructure.SQLDB;

namespace Project.Common.Infrastructure.WebAPI;

public static class ServiceCollectionExtensions
{
    public static void AddCommonWebAPI(this IServiceCollection services)
    {
        services.AddCommonDomain();
        services.AddCommonApplicationService();
        services.AddCommonSQLDB();
        services.AddCustomIdentity();
        services.AddValidator();
    }

    public static void AddCustomIdentity(this IServiceCollection services)
    {
    }

    public static void RegisterCommonMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }

    private static void AddValidator(this IServiceCollection services)
    {
    }
}