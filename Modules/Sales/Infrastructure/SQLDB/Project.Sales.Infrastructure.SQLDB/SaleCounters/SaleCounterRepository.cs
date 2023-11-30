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

        public async Task<OrderDetailInfo> CheckOrderId(int orderId)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                select order_id from order_details
								where order_id = @OrderId
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<OrderDetailInfo>(sql, new
            {
                OrderId = orderId
            });
            return result;
        }

        public async Task CreateOrder(OrderDetailInfo orderDetail)
        {
            await using var connect = await provider.Connect();
            const string sql = @"
                                INSERT [dbo].[orders] 
                                (
	                                 user_id, employee_id, address_id, total
                                )
                                VALUES 
                                (
                                   @UserId, @EmployeeId, @AddressId, @Total
                                )";
            await connect.ExecuteAsync(sql, new
            {
                UserId = orderDetail.UserId,
                EmployeeId = orderDetail.EmployeeId,
                AddressId = orderDetail.AddressId,
                Total = orderDetail.Total,
            });
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

        public async Task<SaleCounterInfo> GetSaleCounterViewId(int productId, int colorId, int sizeId)
        {
            await using var connect = await provider.Connect();

            const string query = @"
            SELECT 
					 pd.id AS ProductDetailId
                    ,p.image AS Image
	                ,p.[name] AS ProductName
	                ,cl.color AS Color
	                ,sz.size AS Size
	                ,pd.price AS Price
                    ,pd.data_version AS DataVersion
	                ,pd.quantity AS Quantity
                FROM 
						product_details AS pd
		                LEFT JOIN products AS p
		                ON pd.product_id = p.id
						LEFT JOIN colors AS cl
						ON pd.color_id = cl.id
						LEFT JOIN sizes AS sz
						ON pd.size_id = sz.id 
						
                WHERE
	                pd.product_id = @ProductId AND pd.color_id = @ColorId AND pd.size_id = @SizeId
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
