using Microsoft.Extensions.DependencyInjection;
using Project.Product.Domain.Colors;
using Project.Product.Domain.Manufacturers;
using Project.Product.Domain.Materials;
using Project.Product.Domain.Products;
using Project.Product.Domain.Trademarks;
using Project.Product.Infrastructure.SQLDB.Colors;
using Project.Product.Infrastructure.SQLDB.Manufacturers;
using Project.Product.Infrastructure.SQLDB.Materials;
using Project.Product.Infrastructure.SQLDB.Products;
using Project.Product.Infrastructure.SQLDB.Trademarks;

namespace Project.Product.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddProductSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IMaterialsRepository, MaterialsRepository>();
        services.AddScoped<ITrademarkRepository, TrademarkRepository>();
    }
}