

namespace Project.Product.Domain.CartDetails
{
    public interface ICartdetailRepository
    {
        #region CartDetail
        Task<IEnumerable<CartdetailInfo>> GetCartdetai();
        Task<IEnumerable<CartDetailView>> GetCartDetails();
        Task CreateCartdetai(CartdetailInfo Cartdetai);
        Task UpdateCartdetai(CartdetailInfo Cartdetai);
        Task DeleteCartdetai(CartdetailInfo Cartdetai);
        Task<CartdetailInfo> CheckCartdetailName(int cartid, int productdetailid);
        Task UpdateQuantityCartdetail(int cartid, int productdetailid);
        #endregion
    
    }
}
