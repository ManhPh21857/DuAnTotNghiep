using Microsoft.Extensions.DependencyInjection;
using Project.Core.Domain;
using Project.Core.Infrastructure.SQLDB;
using Project.Core.Infrastructure.WebAPI.Middlewares.Jwt.Extension;

namespace Project.Core.Infrastructure.WebAPI;

public static class ServiceCollectionExtension
{
    public static void AddCoreWebAPI(this IServiceCollection services)
    {
        services.AddCoreSQLDB();
        services.AddJwt();
        services.AddScoped<ISessionInfo, SessionInfo>();
    }
}