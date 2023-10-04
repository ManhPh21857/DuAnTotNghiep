using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Project.HumanResources.ApplicationService;
using Project.HumanResources.Domain;
using Project.HumanResources.Infrastructure.SQLDB;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Login;
using System.Reflection;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Register;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Accuracy;

namespace Project.HumanResources.Infrastructure.WebAPI;

public static class ServiceCollectionExtensions
{
    public static void AddHumanResourcesWebAPI(this IServiceCollection services)
    {
        services.AddHumanResourcesDomain();
        services.AddHumanResourcesApplicationService();
        services.AddHumanResourcesSQLDB();
        services.AddCustomIdentity();
        services.AddValidator();
    }

    public static void AddCustomIdentity(this IServiceCollection services)
    {
    }

    public static void RegisterHumanResourcesMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }

    private static void AddValidator(this IServiceCollection services)
    {
        services.AddScoped<IValidator<LoginRequestModel>, LoginRequestModelValidator>();
        services.AddScoped<IValidator<RegisterRequestModel>, RegisterRequestModelValidator>();
        services.AddScoped<IValidator<EmailAuthenticationRequestModel>, EmailAuthenticationRequestModelValidator>();
    }
}