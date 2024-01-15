using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.SaleCounters;

namespace Project.Sales.Infrastructure.SQLDB.SaleCounters
{
    public class SaleCounterRepository : ISaleCounterRepository
    {
        private readonly ConnectionProvider provider;

        public SaleCounterRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<OrderDetailInfo> CheckOrderId(int id)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                select id from orders
								where id = @Id
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<OrderDetailInfo>(sql, new
            {
                Id = id
            });
            return result;
        }

        public async Task<int> CreateOrder(OrderInfo param)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
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
                               ,[voucher_id]
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
                               ,0
                               ,0
                               ,@VoucherApplied
                               ,@VoucherId
                               ,@OrderTotal
                               ,@PaymentMethodId
                               ,@OrderDate
                               ,@PaymentDate
                               ,@IsOrdered
                               ,@IsPaid
                               ,@Status
                            )";
            var result = await connect.QueryFirstOrDefaultAsync<int>(sql, new
            {
                OrderCode = param.OrderCode,
                CustomerId = param.CustomerId,
                EmployeeId = param.EmployeeId,
                FullName = param.FullName,
                PhoneNumer = param.PhoneNumber,
                Address = param.Address,
                MerchandiseSubtotal = param.MerchandiseSubtotal,
                VoucherApplied = param.VoucherApplied,
                VoucherId = param.VoucherId,
                OrderTotal = param.OrderTotal,
                PaymentMethodId = param.PaymentMethodId,
                OrderDate = param.OrderDate,
                PaymentDate = param.PaymentDate,
                IsOrdered = param.IsOrder,
                IsPaid = param.IsPaid,
                Status = param.Status,

            });
            return result;
        }

        public async Task CreateOrderDetail(OrderDetailInfo param)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
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
                            )";
            await connect.ExecuteAsync(sql, new
            {
                OrderId = param.OrderId,
                ProductId = param.ProductId,
                ProductName = param.ProductName,
                ColorId = param.ColorId,
                SizeId = param.SizeId,
                Price = param.Price,
                Quantity = param.Quantity
            });
        }

        public async Task<IEnumerable<SaleCounterInfo>> GetSaleCounterView()
        {
            await using var connect = await provider.Connect();

            const string query = @"
            SELECT
	            p.[Id]			 AS Id
               ,p.[Name]		 AS Name
               ,p.[Image]		 AS Image
               ,MIN(pd.[price])	 AS MinPrice
               ,MAX(pd.[price])	 AS MaxPrice
               ,SUM(pd.Quantity) AS Quantity
               ,p.[Description]	 AS Description
            FROM
	            [dbo].[products] AS p
	            LEFT JOIN [dbo].[product_details] AS pd
		            ON p.[Id] = pd.[product_id]
		            AND pd.[is_deleted] = 0
            WHERE pd.is_deleted =0
            GROUP BY
	            p.[Id]
               ,p.[Name]
               ,p.[Image]
               ,p.[Code]
               ,p.[Description]
            ORDER BY
	            p.[Code]
        ";

            var result = await connect.QueryAsync<SaleCounterInfo>(query
            );

            return result;
        }

        public async Task<SaleCounterInfo> GetProductDetailId(int productId, int colorId, int sizeId)
        {
            await using var connect = await provider.Connect();

            const string query = @"
            SELECT 
					 id AS ProductDetailId
                FROM 
						product_details 
						
                WHERE
	                product_id = @ProductId AND color_id = @ColorId AND size_id = @SizeId
        ";

            var result = await connect.QueryFirstOrDefaultAsync<SaleCounterInfo>(query, new
            {
                ProductId = productId,
                ColorId = colorId,
                SizeId = sizeId
            }
            );

            return result;
        }

        public async Task<int> GetQuantity(int productId, int colorId, int sizeId)
        {
            await using var connect = await provider.Connect();

            const string query = @"
            SELECT 
					 quantity
                FROM 
						product_details 
						
                WHERE
	                product_id = @ProductId AND color_id = @ColorId AND size_id = @SizeId
        ";

            var result = await connect.QueryFirstOrDefaultAsync<int>(query, new
            {
                ProductId = productId,
                ColorId = colorId,
                SizeId = sizeId
            }
            );
            return result;
        }
        public async Task UpdateQuantity(UpdateQuantityInfo sale)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                              UPDATE [dbo].[product_details]
                                SET
	                                [quantity] = @Quantity,
                                    [actual_quantity] = @Actual_Quantity
                   
                                WHERE
	                                product_id = @ProductId AND color_id = @ColorId AND size_id = @SizeId
                                    ";
            await connect.ExecuteAsync(sql, new
            {
                ProductId = sale.ProductId,
                ColorId = sale.ColorId,
                SizeId = sale.SizeId,
                Quantity = sale.Quantity,
                Actual_Quantity = sale.Actual_Quantity
            }); ;
        }
    }
}
