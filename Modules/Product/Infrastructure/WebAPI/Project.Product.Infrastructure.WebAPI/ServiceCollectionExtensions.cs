using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.Product.ApplicationService;
using Project.Product.Domain;
using Project.Product.Infrastructure.SQLDB;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Put;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Manufacturers.Post;
using System.Reflection;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Put;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Delete;

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
        // Manufacturer: Nha xan xuat
        services.AddScoped<IValidator<CreateManufacturerModel>, CreateManufacturerModelValidator>();
        services.AddScoped<IValidator<UpdateManufacturerModel>, UpdateManufacturerModelValidator>();
        services.AddScoped<IValidator<DeleteManufacturerModel>, DeleteManufacturerModelValidator>();
        // Materials: Chat lieu
        services.AddScoped<IValidator<CreateMaterialsModel>, CreateMaterialsModelValidator>();
        services.AddScoped<IValidator<UpdateMaterialsModel>, UpdateMaterialsModelValidator>();
        services.AddScoped<IValidator<DeleteMaterialsModel>, DeleteMaterialsModelValidator>();
        // Trademark: Thuong hieu

    }
}