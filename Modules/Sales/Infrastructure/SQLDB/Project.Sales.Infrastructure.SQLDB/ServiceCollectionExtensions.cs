using Microsoft.Extensions.DependencyInjection;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Domain.Carts;
using Project.Sales.Domain.Customers;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Infrastructure.SQLDB.CartDetails;
using Project.Sales.Infrastructure.SQLDB.Carts;
using Project.Sales.Infrastructure.SQLDB.Customers;
using Project.Sales.Infrastructure.SQLDB.SaleCounters;

namespace Project.Sales.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddSalesSQLDB(this IServiceCollection services)
    {
        services.AddScoped<ICartdetailRepository, CartdetailRepository>();
        services.AddScoped<ISaleCounterRepository, SaleCounterRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

    }
}