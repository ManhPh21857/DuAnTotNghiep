using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Project.HumanResources.ApplicationService;

public static class ServiceCollectionExtensions
{
    public static void AddHumanResourcesApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}