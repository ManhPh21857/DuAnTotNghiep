
namespace Project.Sales.Domain.CartDetails
{
    public interface ICartdetailRepository
    {

        Task<IEnumerable<CartDetailInfo>> GetCartdetail(int id);
        Task CreateCartdetai(CartDetailInfo Cartdetai);
        Task UpdateCartdetai(CartDetailInfo Cartdetai);
        Task DeleteCartdetai(CartDetailInfo Cartdetai);

        Task<CartDetailInfo> GetProductdetail(int productid, int colorid, int sizeid);

        Task CreateCartId();
        Task<CartDetailInfo> CheckCartId(int cartid);

        Task<CartDetailInfo> CheckProductDetailId(int cartid, int productdetailid);
        Task UpdateQuantityCartdetail(int cartid, int productdetailid);


    }
}
