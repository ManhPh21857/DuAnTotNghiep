using Dapper;
using Project.Core.Domain.Enums;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Orders;
using Project.Sales.Domain.Vouchers;

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
                   ,[voucher_id]
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
                   ,@VoucherId
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
                    VoucherId = param.VoucherId,
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

        public async Task FinishOrderCashPayment(FinishOrderOnlinePaymentParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE [dbo].[orders]
                SET
                    [payment_date] = @PaymentDate
                   ,[is_paid]	   = @IsPaid
                   ,[status]	   = @Status
                WHERE
	                [id] = @Id
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    PaymentDate = param.PaymentDate,
                    IsPaid = param.IsPaid,
                    Status = param.Status,
                    Id = param.Id
                }
            );
        }

        public async Task<IEnumerable<OrderInfo>> GetOrders(int? customerId, int? orderId)
        {
            await using var connect = await this.provider.Connect();
            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                [Id]				AS Id
                   ,[full_name]			AS FullName
                   ,[order_code]		AS OrderCode
                   ,[order_total]		AS OrderTotal
                   ,[payment_method_id] AS PaymentMethodId
                   ,[is_ordered]		AS IsOrdered
                   ,[is_paid]			AS IsPaid
                   ,[order_date]		AS OrderDate
                   ,[Status]			AS Status
                   ,[voucher_id]		AS VoucherId
                   ,[phone_number]		AS PhoneNumber
                   ,[address]			AS Address
                FROM
	                [dbo].[orders]
                /**where**/
                ORDER BY
	                order_date DESC
            ";

            var template = builder.AddTemplate(query);
            if (customerId.HasValue)
            {
                builder.Where("customer_id = @CustomerId", new { CustomerId = customerId });
            }

            if (orderId.HasValue)
            {
                builder.Where("id = @Id", new { Id = orderId.Value });
            }

            var result = await connect.QueryAsync<OrderInfo>(template.RawSql, template.Parameters);

            return result;
        }

        public async Task<OrderInfo> GetOrder(int id)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [Id]						 AS Id
                   ,[order_code]				 AS OrderCode
                   ,[customer_id]				 AS CustomerId
                   ,[employee_id]				 AS EmployeeId
                   ,[full_name]					 AS FullName
                   ,[phone_number]				 AS PhoneNumber
                   ,[Address]					 AS Address
                   ,[merchandise_subtotal]		 AS MerchandiseSubtotal
                   ,[shipping_fee]				 AS ShippingFee
                   ,[shipping_discount_subtotal] AS ShippingDiscountSubtotal
                   ,[voucher_applied]			 AS VoucherApplied
                   ,[order_total]				 AS OrderTotal
                   ,[payment_method_id]			 AS PaymentMethodId
                   ,[order_date]				 AS OrderDate
                   ,[payment_date]				 AS PaymentDate
                   ,[is_ordered]				 AS IsOrdered
                   ,[is_paid]					 AS IsPaid
                   ,[Status]					 AS Status
                   ,[data_version]				 AS DataVersion
                FROM
	                [dbo].[orders]
                WHERE
                    id = @Id
            ";

            var result = await connect.QueryFirstOrDefaultAsync<OrderInfo>(query,
                new
                {
                    Id = id
                }
            );

            return result;
        }

        public async Task<int?> GetOrderAssign(int? id)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                employee_id
                FROM
	                orders
                WHERE
	                id = @Id
            ";

            int? result = await connect.QueryFirstOrDefaultAsync<int>(query, new { Id = id });

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

        public async Task CancelOrder(int id)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE orders
                SET
	                status = @Status
                WHERE
	                id = @Id
            ";

            await connect.ExecuteAsync(command,
                new
                {
                    Id = id,
                    Status = OrderStatus.Cancel
                }
            );
        }

        public async Task<(IEnumerable<OrderInfo>, int)> GetShopOrder(int skip, int take, int? employeeId)
        {
            await using var connect = await this.provider.Connect();

            var builder = new SqlBuilder();

            const string query = @"
                SELECT
	                o.Id								   AS Id
                   ,o.order_code						   AS OrderCode
                   ,o.customer_id						   AS CustomerId
                   ,CONCAT(c.last_name, ' ', c.first_name) AS CustomerName
                   ,o.full_name							   AS FullName
                   ,o.phone_number						   AS PhoneNumber
                   ,o.[Address]							   AS Address
                   ,o.merchandise_subtotal				   AS MerchandiseSubtotal
                   ,o.order_total						   AS OrderTotal
                   ,o.payment_method_id					   AS PaymentMethodId
                   ,o.is_ordered						   AS IsOrdered
                   ,o.is_paid							   AS IsPaid
                   ,o.Status							   AS Status
                   ,o.employee_id						   AS EmployeeId
                   ,CONCAT(e.last_name, ' ', e.first_name) AS EmployeeName
                   ,o.data_version						   AS DataVersion
                FROM
	                orders AS o
	                LEFT JOIN customers AS c
		                ON o.[customer_id] = c.[Id]
	                LEFT JOIN employees AS e
		                ON o.[employee_id] = e.[Id]
                /**where**/
                ORDER BY
	                o.created_at DESC
                OFFSET @Skip ROWS
                FETCH NEXT @Take ROWS ONLY
                ;
                SELECT
	                COUNT(id) AS Total
                FROM
	                orders
                /**where**/
            
            ";

            var template = builder.AddTemplate(query);
            builder.AddParameters(new
                {
                    Skip = skip,
                    Take = take
                }
            );

            if (employeeId.HasValue)
            {
                builder.Where("o.employee_id = @EmployeeId", new { EmployeeId = employeeId.Value });
            }

            var result = await connect.QueryMultipleAsync(template.RawSql, template.Parameters);

            var orders = result.Read<OrderInfo>();
            int total = result.ReadFirstOrDefault<int>();

            //var result = await connect.QueryAsync<OrderInfo>(template.RawSql, template.Parameters);

            return (orders, total);
        }

        public async Task AssignEmployee(int id, int employeeId, byte[]? dateVersion)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE orders
                SET
	                employee_id = @EmployeeId
                   ,status = @Status
                WHERE
	                id = @Id
                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    EmployeeId = employeeId,
                    Id = id,
                    Status = OrderStatus.Preparing,
                    DataVersion = dateVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateOrderStatus(int id, int status, byte[]? dateVersion)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                UPDATE orders
                SET
	                [status] = @Status
                WHERE
	                id = @Id
	                AND data_version = @DataVersion
            ";

            var result = await connect.ExecuteAsync(command,
                new
                {
                    Id = id,
                    Status = status,
                    DataVersion = dateVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task<IEnumerable<OrderRevenueInfo>> GetOrderRevenue()
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                id			AS Id
                   ,order_total AS OrderTotal
                   ,order_date  AS OrderDate
                FROM
	                orders
                WHERE
	                is_ordered = 1
	                AND status IN @Status
                    AND FORMAT(order_date, 'yyyy/MM/dd') >= @OrderDate 
            ";

            var result = await connect.QueryAsync<OrderRevenueInfo>(query,
                new
                {
                    Status = new[]
                    {
                        OrderStatus.Pending,
                        OrderStatus.NeedToConfirm,
                        OrderStatus.Preparing,
                        OrderStatus.Deliver,
                        OrderStatus.Received
                    },
                    OrderDate = DateTime.Now.AddDays(-15).ToString("yyyy/MM/dd")
                }
            );

            return result;
        }
    }
}
