using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Sales.Domain.CartDetails;

namespace Project.Sales.Infrastructure.SQLDB.CartDetails
{
    public class CartdetailRepository : ICartdetailRepository
    {
        private readonly ConnectionProvider connection;
        public CartdetailRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }

        public async Task<CartDetailInfo> CheckCartdetailName(int cartid, int productdetailid)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                SELECT  product_detail_id As ProductDetailId, cart_id As CartId
                                FROM 
                                cart_details 
                                WHERE cart_details.cart_id =@CartId
                                AND cart_details.product_detail_id = @ProductDetailId
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<CartDetailInfo>(sql, new
            {
                CartId = cartid,
                ProductDetailId = productdetailid
            });
            return result;
        }

        public async Task<IEnumerable<CartDetailInfo>> GetCartdetail(int id)
        {
            var connect = await connection.Connect();
            const string sql = @"
                SELECT 
	                cd.cart_id AS CartId
	                ,cd.product_detail_id AS ProductDetailId
	                ,p.id AS ProductId
	                ,p.[name] AS ProductName
	                ,pd.color_id AS ColorId
	                ,pd.size_id AS SizeId
	                ,pd.price AS Price
	                ,pd.quantity AS Quantity
	                ,pd.data_version AS DataVersion
                FROM 
	                cart_details AS cd
		                left join product_details AS pd
		                ON cd.product_detail_id = pd.id
		                LEFT JOIN products AS p
		                ON pd.product_id = p.id
                WHERE
	                cd.cart_id = @CartId
            ";

            var result = await connect.QueryAsync<CartDetailInfo>(sql,
                new
                {
                    CartId = id
                }
            );

            return result;
        }

       

        public async Task CreateCartdetai(CartDetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[cart_details] 
                                (
	                                cart_id, product_detail_id, price, quantity
                                )
                                VALUES 
                                (
                                   1, @Productdetailid, @Price, @Quantity
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Cartid = Cartdetai.CartId,
                Productdetailid = Cartdetai.ProductDetailId,
                Price = Cartdetai.Price,
                Quantity = Cartdetai.Quantity
            });
        }

        public async Task DeleteCartdetai(CartDetailInfo Cartdetai)
        {
            var connect = await connection.Connect();
            const string sql = @"                            
                               DELETE FROM
	                                cart_details 
                               WHERE 
	                                cart_id = @Cartid AND product_detail_id = @Productdetailid AND data_version = @DataVersion
                                ";
            int result = await connect.ExecuteAsync(sql,
                new
                {
                    Cartid = Cartdetai.CartId,
                    Productdetailid = Cartdetai.ProductDetailId,
                    DataVersion = Cartdetai.DataVersion,
                }
            );

            result.IsOptimisticLocked();
        }
      

        public async Task UpdateCartdetai(CartDetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                               UPDATE  cart_details
			                   SET  product_detail_id =@Productdetailid, quantity =@Quantity, price = @Price
			                   WHERE cart_id =@Cartid AND data_version = @DataVersion
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Cartid = Cartdetai.CartId,
                Productdetailid = Cartdetai.ProductDetailId,
                Price = Cartdetai.Price,
                Quantity = Cartdetai.Quantity,
                DataVersion = Cartdetai.DataVersion
            });
        }

        public async Task UpdateQuantityCartdetail(int cartid, int productdetailid)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                               UPDATE cart_details
								SET quantity = quantity+1
								where cart_id =@CartId AND product_detail_id =@ProductDetailId
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Cartid = cartid,
                ProductDetailId = productdetailid
            });
        }

       
    }
}
