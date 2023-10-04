using Microsoft.Extensions.DependencyInjection;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Infrastructure.SQLDB.Users;

namespace Project.HumanResources.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddHumanResourcesSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}