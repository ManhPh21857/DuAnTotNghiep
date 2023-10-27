

namespace Project.Product.Domain.CartDetails
{
    public interface ICartdetailRepository
    {
        Task<IEnumerable<CartdetailInfo>> GetCartdetai();
        Task CreateCartdetai(CartdetailInfo Cartdetai);
        Task UpdateCartdetai(CartdetailInfo Cartdetai);
        Task DeleteCartdetai(CartdetailInfo Cartdetai);
        Task ReactiveCartdetail(CartdetailInfo cartdetail);
    }
}
