using Dapper;
using Project.Core.Infrastructure.SQLDB.Providers;
using Project.Product.Domain.CartDetails;

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
                                   @Cart_id, @Product_detail_id, @Price, @Quantity
                                )";
            await connect.ExecuteAsync(sql, new
            {

                Cart_id = Cartdetai.Cart_id,
                Product_detail_id = Cartdetai.Product_detail_id,
                Price = Cartdetai.Price,
                Quantity = Cartdetai.Quantity
            });
        }

        public async Task DeleteCartdetai(CartdetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                                DELETE FROM cart_details 
                                WHERE cart_id =@Cart_id AND product_detail_id = @Product_detail_id;
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Cart_id = Cartdetai.Cart_id,
                Product_detail_id = Cartdetai.Product_detail_id
            });
        }

        public async Task<IEnumerable<GetCartdetailInfo>> GetCartdetai()
        {
            var connect = await connection.Connect();
            const string sql = @"
                                SELECT cart_details.cart_id, cart_details.product_detail_id, cart_details.data_version, products.name, 
                                cart_details.price, cart_details.quantity, products.image
                                FROM 
                                cart_details inner join product_details
                                on cart_details.product_detail_id = product_details.id
                                inner join products on product_details.product_id = products.id
                                ";
            var result = await connect.QueryAsync<GetCartdetailInfo>(sql);
            return result;
        }

        public async Task UpdateCartdetai(CartdetailInfo Cartdetai)
        {
            await using var connect = await connection.Connect();
            const string sql = @"
                               UPDATE  product_detail_id =@ProductdetailId, price =@Price, quantity =@Quantity 
                               WHERE cart_id =@CartId and product_detail_id =@Product_detail_id
                                ";
            await connect.ExecuteAsync(sql, new
            {
                Cart_id = Cartdetai.Cart_id,
                Product_detail_id = Cartdetai.Product_detail_id,
                Price = Cartdetai.Price,
                Quantity = Cartdetai.Quantity
            });
        }
    }
}
