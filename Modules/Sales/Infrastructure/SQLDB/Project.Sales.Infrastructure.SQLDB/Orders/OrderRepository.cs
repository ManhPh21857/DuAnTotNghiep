using Dapper;
using Microsoft.IdentityModel.Tokens;
using Project.Core.Domain.Enums;
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
                    [order_code]
	               ,[customer_id]
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
                    @OrderCode
	               ,@CustomerId
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
                    OrderCode = param.OrderCode,
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

        public async Task<IEnumerable<OrderInfo>> GetOrders(int customerId, int? orderId)
        {
            await using var connect = await this.provider.Connect();
            var builder = new SqlBuilder();

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
                /**where**/
                ORDER BY
	                order_date DESC
            ";

            var template = builder.AddTemplate(query);

            builder.Where("customer_id = @CustomerId", new { CustomerId = customerId });
            if (orderId.HasValue)
            {
                builder.Where("id = @Id", new { Id = orderId.Value });
            }  

            var result = await connect.QueryAsync<OrderInfo>(template.RawSql, template.Parameters);

            return result;
        }

        public async Task<IEnumerable<OrderDetailInfo>> GetOrderDetails(int orderId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                od.[order_id]	  AS OrderId
                   ,od.[product_id]	  AS ProductId
                   ,od.[product_name] AS ProductName
                   ,p.[Image]		  AS Image
                   ,od.[color_id]	  AS ColorId
                   ,c.[Color]		  AS Color
                   ,od.[size_id]	  AS SizeId
                   ,s.[Size]		  AS Size
                   ,od.[Price]		  AS Price
                   ,od.[Quantity]	  AS Quantity
                FROM
	                [dbo].[order_details] AS od
	                LEFT JOIN [dbo].[products] AS p
		                ON od.product_id = p.id
	                LEFT JOIN [dbo].[colors] AS c
		                ON od.color_id = c.id
	                LEFT JOIN [dbo].[sizes] AS s
		                ON od.size_id = s.id
                WHERE
	                od.[order_id] = @OrderId
            ";

            var result = await connect.QueryAsync<OrderDetailInfo>(query,
                new
                {
                    OrderId = orderId
                }
            );

            return result;
        }

        public async Task CancelOrder(int id, int customerId)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE orders
                SET
	                status = @Status
                WHERE
	                id = @Id
                    AND customer_id = @CustomerId
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    Id = id,
                    CustomerId = customerId,
                    Status = OrderStatus.Cancel
                }
            );
        }

        public async Task<(IEnumerable<OrderInfo>, int)> GetShopOrder(int skip, int take, GetOrderFilter? param)
        {
            await using var connect = await this.provider.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
                    id                   AS Id
	               ,order_code			 AS OrderCode
                   ,full_name			 AS FullName
                   ,phone_number		 AS PhoneNumber
                   ,[Address]			 AS Address
                   ,merchandise_subtotal AS MerchandiseSubtotal
                   ,order_total			 AS OrderTotal
                   ,payment_method_id	 AS PaymentMethodId
                   ,is_ordered			 AS IsOrdered
                   ,is_paid				 AS IsPaid
                   ,Status				 AS Status
                   ,data_version		 AS DataVersion
                FROM
	                orders
                /**where**/
                ORDER BY
	                created_at DESC
                OFFSET @Skip ROWS
                FETCH NEXT @Take ROWS ONLY
                ;
                SELECT
	                COUNT(id) AS Total
                FROM
	                orders
            ";

            var template = builder.AddTemplate(query);
            builder.AddParameters(new
                {
                    Skip = skip,
                    Take = take
                }
            );

            if (param is not null)
            {
                if (!param.Name.IsNullOrEmpty())
                {
                    builder.Where("full_name like @Name", new { Name = $"%{param.Name}%" });
                }

                if (param.From.HasValue)
                {
                    builder.Where("order_date >= @From", new { From =param.From.Value.ToString("MM-dd-yyyy") });
                }

                if (param.To.HasValue)
                {
                    builder.Where("order_date <= @To", new { To = param.To.Value.ToString("MM-dd-yyyy") });
                }

                if (!param.ListStatus.IsNullOrEmpty())
                {
                    builder.Where("status IN @ListStatus", new { ListStatus = param.ListStatus });
                }
            }

            var result = await connect.QueryMultipleAsync(template.RawSql, template.Parameters);

            var orders = result.Read<OrderInfo>();
            int total = result.ReadFirstOrDefault<int>();

            //var result = await connect.QueryAsync<OrderInfo>(template.RawSql, template.Parameters);

            return (orders, total);
        }
    }
}
