using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Orders;

namespace Project.Sales.Infrastructure.SQLDB.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ConnectionProvider provider;

        public OrderRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<int> CreateOrder(CreateOrderParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[orders] (
	                [customer_id]
                   ,[employee_id]
                   ,[full_name]
                   ,[phone_number]
                   ,[address]
                   ,[merchandise_subtotal]
                   ,[shipping_fee]
                   ,[shipping_discount_subtotal]
                   ,[voucher_applied]
                   ,[order_total]
                   ,[payment_method_id]
                   ,[order_date]
                   ,[payment_date]
                   ,[is_ordered]
                   ,[is_paid]
                   ,[status]
                )
                OUTPUT Inserted.Id
                VALUES (
	                @CustomerId
                   ,@EmployeeId
                   ,@FullName
                   ,@PhoneNumer
                   ,@Address
                   ,@MerchandiseSubtotal
                   ,@ShippingFee
                   ,@ShippingDiscountSubtotal
                   ,@VoucherApplied
                   ,@OrderTotal
                   ,@PaymentMethodId
                   ,@OrderDate
                   ,@PaymentDate
                   ,@IsOrdered
                   ,@IsPaid
                   ,@Status
                )
            ";

            var orderId = await connect.QueryFirstOrDefaultAsync<int>(command,
                new
                {
                    CustomerId = param.CustomerId,
                    EmployeeId = param.EmployeeId,
                    FullName = param.FullName,
                    PhoneNumer = param.PhoneNumber,
                    Address = param.Address,
                    MerchandiseSubtotal = param.MerchandiseSubtotal,
                    ShippingFee = param.ShippingFee,
                    ShippingDiscountSubtotal = param.ShippingDiscountSubtotal,
                    VoucherApplied = param.VoucherApplied,
                    OrderTotal = param.OrderTotal,
                    PaymentMethodId = param.PaymentMethodId,
                    OrderDate = param.OrderDate,
                    PaymentDate = param.PaymentDate,
                    IsOrdered = param.IsOrder,
                    IsPaid = param.IsPaid,
                    Status = param.Status,
                }
            );

            return orderId;
        }

        public async Task CreateOrderDetail(CreateOrderDetailParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                INSERT INTO [dbo].[order_details] (
	                [order_id]
                   ,[product_id]
                   ,[product_name]
                   ,[color_id]
                   ,[size_id]
                   ,[price]
                   ,[quantity]
                )
                VALUES (
	                @OrderId
                   ,@ProductId
                   ,@ProductName
                   ,@ColorId
                   ,@SizeId
                   ,@Price
                   ,@Quantity
                )
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    OrderId = param.OrderId,
                    ProductId = param.ProductId,
                    ProductName = param.ProductName,
                    ColorId = param.ColorId,
                    SizeId = param.SizeId,
                    Price = param.Price,
                    Quantity = param.Quantity
                }
            );
        }

        public async Task FinishOrderOnlinePayment(FinishOrderOnlinePaymentParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE [dbo].[orders]
                SET
	                [order_date]   = @OrderDate
                   ,[payment_date] = @PaymentDate
                   ,[is_ordered]   = @IsOrdered
                   ,[is_paid]	   = @IsPaid
                   ,[status]	   = @Status
                WHERE
	                [id] = @Id
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    OrderDate = param.OrderDate,
                    PaymentDate = param.PaymentDate,
                    IsOrdered = param.IsOrdered,
                    IsPaid = param.IsPaid,
                    Status = param.Status,
                    Id = param.Id
                }
            );
        }

        public async Task<IEnumerable<OrderInfo>> GetOrders(int customerId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]				AS Id
                   ,[order_total]		AS OrderTotal
                   ,[payment_method_id] AS PaymentMethodId
                   ,[is_ordered]		AS IsOrdered
                   ,[is_paid]			AS IsPaid
                   ,[order_date]		AS OrderDate
                   ,[Status]			AS Status
                FROM
	                [dbo].[orders]
                WHERE
	                customer_id = @CustomerId
            ";

            var result = await connect.QueryAsync<OrderInfo>(query,
                new
                {
                    CustomerId = customerId
                }
            );

            return result;
        }
        
        public async Task<IEnumerable<OrderDetailInfo>> GetOrderDetails(int orderId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [order_id]	   AS OrderId
                   ,[product_id]   AS ProductId
                   ,[product_name] AS ProductName
                   ,[color_id]	   AS ColorId
                   ,[size_id]	   AS SizeId
                   ,[Price]		   AS Price
                   ,[Quantity]	   AS Quantity
                FROM
	                [dbo].[order_details]
                WHERE
	                [order_id] = @OrderId
            ";

            var result = await connect.QueryAsync<OrderDetailInfo>(query,
                new
                {
                    OrderId = orderId
                }
            );

            return result;
        }
    }
}
