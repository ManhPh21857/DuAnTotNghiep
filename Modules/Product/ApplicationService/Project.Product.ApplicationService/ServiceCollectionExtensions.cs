using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace Project.Product.ApplicationService;

public static class ServiceCollectionExtensions
{
    public static void AddProductApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}