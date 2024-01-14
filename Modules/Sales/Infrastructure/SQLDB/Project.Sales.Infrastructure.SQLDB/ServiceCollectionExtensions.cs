using Microsoft.Extensions.DependencyInjection;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Domain.Carts;
using Project.Sales.Domain.Customers;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Domain.Orders;
using Project.Sales.Domain.Payments;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Infrastructure.SQLDB.CartDetails;
using Project.Sales.Infrastructure.SQLDB.Carts;
using Project.Sales.Infrastructure.SQLDB.Customers;
using Project.Sales.Infrastructure.SQLDB.Dashboards;
using Project.Sales.Infrastructure.SQLDB.Orders;
using Project.Sales.Infrastructure.SQLDB.Payments;
using Project.Sales.Infrastructure.SQLDB.SaleCounters;
using Project.Sales.Infrastructure.SQLDB.Vouchers;

namespace Project.Sales.Infrastructure.SQLDB;

public static class ServiceCollectionExtensions
{
    public static void AddSalesSQLDB(this IServiceCollection services)
    {
        services.AddScoped<ICartdetailRepository, CartdetailRepository>();
        services.AddScoped<ISaleCounterRepository, SaleCounterRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IDashboardsRepository, DashboardRepository>();
    }
}