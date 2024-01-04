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
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Forgot;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Post;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Put;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Put;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Post;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Put;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Post;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Delete;

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
        services.AddScoped<IValidator<ForgotRequestModel>, ForgotRequestModelValidator>();
        services.AddScoped<IValidator<UpdateEmployeeRequestModel>, UpdateEmployeeRequestModelValidator>();
        services.AddScoped<IValidator<DeleteEmployeeModel>, DeleteEmployeeModelValidator>();
        services.AddScoped<IValidator<DeleteEmployeeRequestModel>, DeleteEmployeeRequestModelValidator>();
        services.AddScoped<IValidator<UpdateCustomerModel>, UpdateCustomerModelValidator>();
        services.AddScoped<IValidator<UpdateAddressRequestModel>, UpdateAddressRequestModelValidator>();
        services.AddScoped<IValidator<UpdateDefaultAddressRequest>, UpdateDefaultAddressRequestValidator>();
        services.AddScoped<IValidator<ChangePasswordRequestModel>, ChangePasswordRequestModelValidator>();
        services.AddScoped<IValidator<UpdateGroupRoleRequestModel>, UpdateGroupRoleRequestModelValidator>();
        services.AddScoped<IValidator<DeleteGroupModel>, DeleteGroupModelValidator>();
        services.AddScoped<IValidator<DeleteGroupRequestModel>, DeleteGroupRequestModelValidator>();
    }
}