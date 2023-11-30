using Microsoft.Extensions.DependencyInjection;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Infrastructure.SQLDB.Customers;
using Project.HumanResources.Infrastructure.SQLDB.Employees;
using Project.HumanResources.Infrastructure.SQLDB.Roles;
using Project.HumanResources.Infrastructure.SQLDB.Users;

namespace Project.HumanResources.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddHumanResourcesSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}