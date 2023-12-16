using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.Carts;

namespace Project.Sales.Infrastructure.SQLDB.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly ConnectionProvider provider;

        public CartRepository(ConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task<int> GetCountItem(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                SUM(cd.quantity) AS Count
                FROM
	                carts AS cart
	                LEFT JOIN customers AS cus
		                ON cart.customer_id = cus.Id
	                LEFT JOIN cart_details AS cd
		                ON cart.Id = cd.cart_id
                WHERE
	                cus.user_id = @UserId
	                AND cus.is_deleted = 0
            ";

            int? result = await connect.QueryFirstOrDefaultAsync<int?>(query,
                new
                {
                    UserId = userId
                }
            );

            return result ?? 0;
        }

        public async Task<int?> FindCartId(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                cart.id AS Id
                FROM
	                carts AS cart
	                LEFT JOIN customers AS cus
		                ON cart.customer_id = cus.id
                WHERE
	                cus.user_id = @UserId
	                AND cus.is_deleted = 0
            ";

            int? result = await connect.QueryFirstOrDefaultAsync<int?>(query,
                new
                {
                    UserId = userId
                }
            );

            return result;
        }

        public async Task<IEnumerable<CartDetailInfo>> GetCartDetail(int id)
        {
            var connect = await this.provider.Connect();
            const string sql = @"
                SELECT
	                cd.cart_id			 AS CartId
                   ,cd.product_detail_id AS ProductDetailId
                   ,p.id				 AS ProductId
                   ,pd.color_id			 AS ColorId
                   ,pd.size_id			 AS SizeId
                   ,c.Color				 AS Color
                   ,s.Size				 AS Size
                   ,p.Image				 AS Image
                   ,p.[name]			 AS ProductName
                   ,pd.Price			 AS Price
                   ,cd.Quantity			 AS Quantity
                   ,cd.data_version		 AS DataVersion
                FROM
	                cart_details AS cd
	                LEFT JOIN carts AS cart
		                ON cd.cart_id = cart.id
	                LEFT JOIN product_details AS pd
		                ON cd.product_detail_id = pd.id
	                LEFT JOIN products AS p
		                ON pd.product_id = p.id
	                LEFT JOIN colors AS c
		                ON pd.color_id = c.id
	                LEFT JOIN sizes AS s
		                ON pd.size_id = s.id
                WHERE
	                cart.id = @CartId
            ";

            var result = await connect.QueryAsync<CartDetailInfo>(sql,
                new
                {
                    CartId = id
                }
            );

            return result;
        }

        public async Task<int> GetCartId(int userId)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                DECLARE @CartId INT;
                SET @CartId = (
	                SELECT
		                cart.id
	                FROM
		                carts AS cart
		                LEFT JOIN customers AS cus
			                ON cart.customer_id = cus.id
	                WHERE
		                cus.user_id = @userId
                )
                IF @CartId is null
	                INSERT INTO [dbo].[carts] (
		                [customer_id]
	                )
	                OUTPUT INSERTED.id AS CartId
	                VALUES (
		                (
			                SELECT
				                id
			                FROM
				                customers
			                WHERE
				                user_id = @userId
		                )
	                )
                ELSE
                SELECT	@CartId AS CartId
            ";

            var result = await connect.QueryFirstOrDefaultAsync<int>(command,
                new
                {
                    UserId = userId
                }
            );

            return result;
        }

        public async Task<CartDetail> FindCartDetail(int cartId, int productDetailId)
        {
            await using var connect = await this.provider.Connect();

            const string query = @"
                SELECT
	                [cart_id]			AS CartId
                   ,[product_detail_id] AS ProductDetailId
                   ,[quantity]			AS Quantity
                FROM
	                [cart_details]
                WHERE
	                [cart_id] = @CartId
	                AND [product_detail_id] = @ProductDetailId
            ";

            var result = await connect.QueryFirstOrDefaultAsync<CartDetail>(query,
                new
                {
                    CartId = cartId,
                    ProductDetailId = productDetailId
                }
            );

            return result;
        }

        public async Task CreateCartDetail(CreateCartDetailParam param)
        {
            await using var connect = await this.provider.Connect();

            const string sql = @"
                INSERT INTO [dbo].[cart_details] (
	                [cart_id]
                   ,[product_detail_id]
                   ,[quantity]
                )
                VALUES (
	                @CartId
                   ,@ProductDetailId
                   ,@Quantity
                )
            ";

            await connect.ExecuteAsync(sql,
                new
                {
                    CartId = param.CartId,
                    ProductDetailId = param.ProductDetailId,
                    Quantity = param.Quantity
                }
            );
        }

        public async Task UpdateCartDetail(CreateCartDetailParam param)
        {
            await using var connect = await this.provider.Connect();

            const string sql = @"
                UPDATE [dbo].[cart_details]
                SET
	                [quantity] = @Quantity

                WHERE
	                cart_id = @CartId
	                AND product_detail_id = @ProductDetailId
            ";

            int result = await connect.ExecuteAsync(sql,
                new
                {
                    Quantity = param.Quantity,
                    CartId = param.CartId,
                    ProductDetailId = param.ProductDetailId
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task DeleteCartDetail(DeleteCartDetailParam param)
        {
            await using var connect = await this.provider.Connect();

            const string command = @"
                DELETE FROM [dbo].[cart_details]
                WHERE
	                cart_id = @CartId
	                AND product_detail_id = @ProductDetailId
	                AND data_version = @DataVersion
            ";

            int result = await connect.ExecuteAsync(command,
                new
                {
                    CartId = param.CartId,
                    ProductDetailId = param.ProductDetailId,
                    DataVersion = param.DataVersion
                }
            );

            result.IsOptimisticLocked();
        }
    }
}
