using Microsoft.Extensions.DependencyInjection;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Infrastructure.SQLDB.CartDetails;

namespace Project.Sales.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddSalesSQLDB(this IServiceCollection services)
    {
        services.AddScoped<ICartdetailRepository, CartdetailRepository>();
    }
}