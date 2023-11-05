using Dapper;
using Project.Core.Infrastructure.SQLDB.Extensions;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.CartDetails;
using Project.Product.Domain.Enums;

namespace Project.Product.Infrastructure.SQLDB.CartDetails
{
    public class CartdetailRepository : ICartdetailRepository
    {
        private readonly ConnectionProvider connection;
        public CartdetailRepository(ConnectionProvider connection)
        {
            this.connection = connection;
        }

        public async Task<CartdetailInfo> CheckCartdetailName(int cartid, int productdetailid)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                SELECT  product_detail_id As ProductDetailId, cart_id As CartId
                                FROM 
                                cart_details 
                                WHERE cart_details.cart_id =@CartId
                                AND cart_details.product_detail_id = @ProductDetailId
                                ";
            var result = await connect.QueryFirstOrDefaultAsync<CartdetailInfo>(sql, new
            {
                CartId = cartid,
                ProductDetailId = productdetailid
            });
            return result;
        }


        public async Task<IEnumerable<CartdetailInfo>> GetCartdetai()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                SELECT 
                                       cart_id As CartId, product_detail_id As ProductDetailId,
								       price As Price, quantity As Quantity, data_version As DataVersion
								FROM 
                                       cart_details
                                Where
                                      cart_id = @CartId and product_detail_id = @ProductDetailId
                                ";

            var result = await connect.QueryAsync<CartdetailInfo>(sql);

            return result;
        }

        public async Task<IEnumerable<CartDetailView>> GetCartDetails()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                SELECT cart_details.cart_id As CartId, cart_details.product_detail_id As ProductDetailId, 
								products.image As Image,
								cart_details.data_version As DataVersion, products.name As Name, 
								colors.color As Color, sizes.size As Size,
                                cart_details.quantity As Quantity, cart_details.price As Price
                                FROM 
                                cart_details left join product_details
                                on cart_details.product_detail_id = product_details.id
								left join colors on product_details.color_id = colors.id
								left join sizes on product_details.size_id = sizes.id
                                left join products on product_details.product_id = products.id
                                ";

            var result = await connect.QueryAsync<CartDetailView>(sql);

            return result;
        }


        public async Task CreateCartdetai(CartdetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                INSERT [dbo].[cart_details] 
                                (
	                                cart_id, product_detail_id, price, quantity
                                )
                                VALUES 
                                (
                                   @Cartid, @Productdetailid, @Price, @Quantity
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Cartid = Cartdetai.CartId,
                Productdetailid = Cartdetai.ProductDetailId,
                Price = Cartdetai.Price,
                Quantity = Cartdetai.Quantity
            });
        }

        public async Task DeleteCartdetai(CartdetailInfo Cartdetai)
        {
            var connect = await connection.Connect();
            const string sql = @"                            
                               DELETE FROM
	                                cart_details 
                               WHERE 
	                                cart_id = @CartId AND product_detail_id = @ProductDetailId AND data_version = @DataVersion
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
      

        public async Task UpdateCartdetai(CartdetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                               UPDATE  cart_details
			                   SET  product_detail_id =@Productdetailid, quantity =@Quantity, price = @Price
			                   WHERE cart_id =@Cartid and product_detail_id =@Productdetailid AND data_version = @DataVersion
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
