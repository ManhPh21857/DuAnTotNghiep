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

        public async Task<int> CreateOrder(OrderInfo order)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                INSERT [dbo].[orders] 
                                (
	                                 customer_id, employee_id, address_id, total
                                )
                                OUTPUT Inserted.ID
                                VALUES 
                                (
                                   @CustomerId, @EmployeeId, @AddressId, @Total
                                )";
            var result = await connect.QueryFirstOrDefaultAsync<int>(sql, new
            {

                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                AddressId = order.AddressId,
                Total = order.Total,

            });
            return result;
        }

        public async Task CreateOrderDetail(OrderDetailInfo orderDetail)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                INSERT [dbo].[order_details] 
                                (
	                                 order_id, product_detail_id, voucher_id, price, quantity
                                )
                                VALUES 
                                (
                                   @OrderId, @ProductDetailId, @VoucherId, @Price, @Quantity
                                )";
            await connect.ExecuteAsync(sql, new
            {
                OrderId = orderDetail.OrderId,
                ProductDetailId = orderDetail.ProductDetailId,
                VoucherId = orderDetail.VoucherId,
                Price = orderDetail.Price,
                Quantity = orderDetail.Quantity
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
    }
}
