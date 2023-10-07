using Microsoft.Extensions.DependencyInjection;
using Project.Product.Domain.Colors;
using Project.Product.Infrastructure.SQLDB.Colors;

namespace Project.Product.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddProductSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IColorRepository, ColorRepository>();
    }
}