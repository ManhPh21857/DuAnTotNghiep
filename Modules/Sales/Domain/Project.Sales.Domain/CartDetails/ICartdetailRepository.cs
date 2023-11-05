

namespace Project.Sales.Domain.CartDetails
{
    public interface ICartdetailRepository
    {

        Task<IEnumerable<CartDetailInfo>> GetCartdetail();
        Task CreateCartdetai(CartDetailInfo Cartdetai);
        Task UpdateCartdetai(CartDetailInfo Cartdetai);
        Task DeleteCartdetai(CartDetailInfo Cartdetai);
        Task<CartDetailInfo> CheckCartdetailName(int cartid, int productdetailid);
        Task UpdateQuantityCartdetail(int cartid, int productdetailid);
    
    }
}
