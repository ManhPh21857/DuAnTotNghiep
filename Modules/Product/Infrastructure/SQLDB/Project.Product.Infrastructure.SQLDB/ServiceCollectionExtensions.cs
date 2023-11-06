using Microsoft.Extensions.DependencyInjection;
using Project.Product.Domain.Classifications;
using Project.Product.Domain.Colors;
using Project.Product.Domain.Images;
using Project.Product.Domain.Materials;
using Project.Product.Domain.Origins;
using Project.Product.Domain.Products;
using Project.Product.Domain.Sizes;
using Project.Product.Domain.Suppliers;
using Project.Product.Domain.Trademarks;
using Project.Product.Infrastructure.SQLDB.Classifications;
using Project.Product.Infrastructure.SQLDB.Colors;
using Project.Product.Infrastructure.SQLDB.Images;
using Project.Product.Infrastructure.SQLDB.Materials;
using Project.Product.Infrastructure.SQLDB.Origins;
using Project.Product.Infrastructure.SQLDB.Products;
using Project.Product.Infrastructure.SQLDB.Sizes;
using Project.Product.Infrastructure.SQLDB.Suppliers;
using Project.Product.Infrastructure.SQLDB.Trademarks;

namespace Project.Product.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddProductSQLDB(this IServiceCollection services)
    {
        services.AddScoped<ISizeRepository, SizeRepository>();
        services.AddScoped<IColorRepository, ColorRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IMaterialRepository, MaterialRepository>();
        services.AddScoped<ITrademarkRepository, TrademarkRepository>();
        services.AddScoped<IOriginRepository, OriginRepository>();
        services.AddScoped<IClassificationRepository, ClassificationRepository>();
    }
}