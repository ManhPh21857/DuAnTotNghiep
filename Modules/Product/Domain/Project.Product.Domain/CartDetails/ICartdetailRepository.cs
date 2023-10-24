

namespace Project.Product.Domain.CartDetails
{
    public interface ICartdetailRepository
    {
        Task<IEnumerable<GetCartdetailInfo>> GetCartdetai();
        Task CreateCartdetai(CartdetailInfo Cartdetai);
        Task UpdateCartdetai(CartdetailInfo Cartdetai);
        Task DeleteCartdetai(CartdetailInfo Cartdetai);
    }
}
