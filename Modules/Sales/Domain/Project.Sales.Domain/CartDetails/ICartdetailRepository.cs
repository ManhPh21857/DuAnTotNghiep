
namespace Project.Sales.Domain.CartDetails
{
    public interface ICartdetailRepository
    {

        Task<IEnumerable<CartDetailInfo>> GetCartdetail(int id);
        Task CreateCartdetai(CartDetailInfo Cartdetai);
        Task UpdateCartdetai(CartDetailInfo Cartdetai);
        Task DeleteCartdetai(CartDetailInfo Cartdetai);
        Task<CartDetailInfo> CheckCartdetailName(int cartid, int productdetailid);
        Task UpdateQuantityCartdetail(int cartid, int productdetailid);


    }
}
