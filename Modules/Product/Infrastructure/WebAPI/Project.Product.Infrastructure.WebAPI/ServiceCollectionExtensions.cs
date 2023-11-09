using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Product.ApplicationService;
using Project.Product.Domain;
using Project.Product.Infrastructure.SQLDB;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Post;
using System.Reflection;

namespace Project.Product.Infrastructure.WebAPI;

public static class ServiceCollectionExtensions
{
    public static void AddProductWebAPI(this IServiceCollection services)
    {
        services.AddProductDomain();
        services.AddProductApplicationService();
        services.AddProductSQLDB();
        services.AddCustomIdentity();
        services.AddValidator();
    }

    public static void AddCustomIdentity(this IServiceCollection services)
    {
    }

    public static void RegisterProductMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }

    public static void AddValidator(this IServiceCollection services)
    {
        // Supplier: Nha xan xuat
        services.AddScoped<IValidator<UpdateSupplierModel>, UpdateSupplierModelValidator>();
        services.AddScoped<IValidator<UpdateSupplierRequestModel>, UpdateSupplierRequestModelValidator>();
        // Materials: Chat lieu
        services.AddScoped<IValidator<UpdateMaterialModel>, UpdateMaterialModelValidator>();
        services.AddScoped<IValidator<UpdateMaterialRequestModel>, UpdateMaterialRequestModelValidator>();
        // Trademark: Thuong hieu
        services.AddScoped<IValidator<UpdateTrademarkModel>, UpdateTrademarkModelValidator>();
        services.AddScoped<IValidator<UpdateTrademarkRequestModel>, UpdateTrademarkRequestModelValidator>();
        // Origin: Xuất xứ
        services.AddScoped<IValidator<UpdateOriginModel>, UpdateOriginModelValidator>();
        services.AddScoped<IValidator<UpdateOriginRequestModel>, UpdateOriginRequestModelValidator>();

        //Color
        services.AddScoped<IValidator<UpdateColorRequestModel>, UpdateColorRequestModelValidator>();
        services.AddScoped<IValidator<UpdateColorModel>, UpdateColorModelValidator>();

        //Size 
        services.AddScoped<IValidator<UpdateSizeRequestModel>, UpdateSizeRequestModelValidator>();
        services.AddScoped<IValidator<UpdateSizeModel>, UpdateSizeModelValidator>();

        //Classification 
        services.AddScoped<IValidator<UpdateClassificationsRequestModel>, UpdateClassificationsRequestModelValidator>();
        services.AddScoped<IValidator<UpdateClassificationsModel>, UpdateClassificationsModelValidator>();

        //Product
        services.AddScoped<IValidator<UpdateProductModel>, UpdateProductModelValidator>();
        services.AddScoped<IValidator<UpdateProductColorModel>, UpdateProductColorModelValidator>();
        services.AddScoped<IValidator<UpdateProductSizeModel>, UpdateProductSizeModelValidator>();
        services.AddScoped<IValidator<UpdateProductDetailModel>, UpdateProductDetailModelValidator>();
        services.AddScoped<IValidator<UpdateProductRequestModel>, UpdateProductRequestModelValidator>();
    }
}