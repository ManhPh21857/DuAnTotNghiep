using Microsoft.Extensions.DependencyInjection;
using Project.Product.Domain.Colors;
using Project.Product.Domain.Manufacturers1;
using Project.Product.Infrastructure.SQLDB.Colors;
using Project.Product.Infrastructure.SQLDB.Manufacturers1;

namespace Project.Product.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddProductSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
    }
}