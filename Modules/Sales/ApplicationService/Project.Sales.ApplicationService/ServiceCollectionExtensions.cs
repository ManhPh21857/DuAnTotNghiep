using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Project.Sales.ApplicationService;

public static class ServiceCollectionExtensions
{
    public static void AddSalesApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}