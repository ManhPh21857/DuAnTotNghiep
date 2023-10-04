using Microsoft.Extensions.DependencyInjection;
using Project.Common.Domain.Products;
using Project.Common.Infrastructure.SQLDB.Products;

namespace Project.Common.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddCommonSQLDB(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}