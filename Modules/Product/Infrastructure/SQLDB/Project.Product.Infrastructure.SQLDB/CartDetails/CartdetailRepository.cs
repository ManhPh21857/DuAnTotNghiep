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

        public async Task<IEnumerable<CartdetailInfo>> GetCartdetai()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                SELECT cart_details.cart_id As CartId, cart_details.product_detail_id As ProductDetailId, 
								cart_details.data_version As DataVersion, products.name As Name, 
                                cart_details.price As Price, cart_details.quantity As Quantity, 
								products.image As Image
                                FROM 
                                cart_details inner join product_details
                                on cart_details.product_detail_id = product_details.id
                                inner join products on product_details.product_id = products.id
                                ";
           
            var result = await connect.QueryAsync<CartdetailInfo>(sql);

            return result;
        }

        public async Task ReactiveCartdetail(CartdetailInfo cartdetail)
        {
            var connect = await connection.Connect();

            const string query = @"
                UPDATE [dbo].[cart_details]
                SET
	                [is_deleted] = @NotDeleted
                WHERE
	                [cart_id] = @Cartid
                    AND [product_detail_id] = @Productdetailid
                    AND [data_version] = @DataVersion
                    AND [is_deleted] = @IsDeleted
            ";

            int result = await connect.ExecuteAsync(query,
                new
                {
                    IsDeleted = IsDeleted.Yes,
                    Id = cartdetail.CartId,
                    Productdetailid = cartdetail.ProductDetailId,
                    DataVersion = cartdetail.DataVersion,
                    NotDeleted = IsDeleted.No
                }
            );

            result.IsOptimisticLocked();
        }

        public async Task UpdateCartdetai(CartdetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                               UPDATE  product_detail_id =@ProductdetailId, price =@Price, quantity =@Quantity 
                               WHERE cart_id =@CartId 
                               AND product_detail_id =@Productdetailid 
                               AND [data_version] = @DataVersion
                               AND [is_deleted] = @IsDeleted
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Cartid = Cartdetai.CartId,
                Productdetailid = Cartdetai.ProductDetailId,
                Price = Cartdetai.Price,
                Quantity = Cartdetai.Quantity,
                DataVersion = Cartdetai.DataVersion,
                IsDeleted = IsDeleted.No
            });
        }
    }
}
