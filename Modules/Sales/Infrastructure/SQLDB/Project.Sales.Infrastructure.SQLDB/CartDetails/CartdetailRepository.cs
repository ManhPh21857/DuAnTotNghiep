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

        public async Task<CartDetailInfo> CheckProductDetailId(int cartid, int productdetailid)
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
                    ,p.image AS Image
	                ,p.[name] AS ProductName
	                ,cl.color AS Color
	                ,sz.size AS Size
	                ,pd.price AS Price
	                ,cd.quantity AS Quantity
	                ,cd.data_version AS DataVersion
                FROM 
	                cart_details AS cd
		                left join product_details AS pd
		                ON cd.product_detail_id = pd.id
		                LEFT JOIN products AS p
		                ON pd.product_id = p.id
						LEFT JOIN colors AS cl
						ON pd.color_id = cl.id
						LEFT JOIN sizes AS sz
						ON pd.size_id = sz.id 
						LEFT JOIN carts AS ct
						ON ct.id = cd.cart_id
                WHERE
	                ct.id= @Cartid
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
                                   @Cartid, @Productdetailid, 1, @Quantity
                                )";
            await connect.ExecuteAsync(sql, new
            {
                Cartid = Cartdetai.CartId,
                Productdetailid = Cartdetai.ProductDetailId,
                Quantity = Cartdetai.Quantity
            });
        }

        public async Task DeleteCartdetai(CartDetailInfo Cartdetai)
        {
            var connect = await connection.Connect();
            const string sql = @"                            
                               Delete FROM 
	                                cart_details 
                                
                               WHERE 
	                                cart_id = @Cartid AND product_detail_id = @Productdetailid AND data_version = @DataVersion
                                ";
            int result = await connect.ExecuteAsync(sql,
                new
                {
                    Cartid = Cartdetai.CartId,
                    Productdetailid = Cartdetai.ProductDetailId,
                    DataVersion = Cartdetai.DataVersion
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateCartdetai(CartDetailInfo Cartdetai)
        {
            var connect = await connection.Connect();
            const string sql = @"                            
                               Delete FROM 
	                                cart_details 
                                
                               WHERE 
	                                cart_id = @Cartid AND product_detail_id = @Productdetailid AND data_version = @DataVersion
                                ";
            int result = await connect.ExecuteAsync(sql,
                new
                {
                    Cartid = Cartdetai.CartId,
                    Productdetailid = Cartdetai.ProductDetailId,
                    DataVersion = Cartdetai.DataVersion
                }
            );
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

        public async Task CreateCartId()
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[carts] 
                                (
	                                user_id
                                )
                                VALUES 
                                (
                                     1
                                )";
            await connect.ExecuteAsync(sql);
        }

        public async Task<CartDetailInfo> CheckCartId(int cartid)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                SELECT id FROM carts 
                                WHERE id = @Cartid
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<CartDetailInfo>(sql, new
            {
                CartId = cartid
            });
            return result;
        }

        public async Task<CartDetailInfo> GetProductdetail(int productid, int colorid, int sizeid)
        {
            
                await using var connect = await connection.Connect();
            const string sql = @"
                                SELECT id FROM product_details
                                WHERE product_id = @Productid AND size_id = @Sizeid AND color_id = @Colorid
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<CartDetailInfo>(sql, new
            {
                Productid = productid,
                Colorid = colorid,
                Sizeid = sizeid
            });
            return result;
        }

    }
}
