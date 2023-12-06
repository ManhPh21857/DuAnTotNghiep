namespace Project.Sales.Domain.Carts
{
    public interface ICartRepository
    {
        Task<int> GetCountItem(int userId);
        Task<int> FindCartId(int userId);
        Task<IEnumerable<CartDetailInfo>> GetCartDetail(int id);
        Task<int> GetCartId(int userId);
        Task<CartDetail> FindCartDetail(int cartId, int productDetailId);
        Task CreateCartDetail(CreateCartDetailParam param);
        Task UpdateCartDetail(CreateCartDetailParam param);
        Task DeleteCartDetail(DeleteCartDetailParam param);
    }
}
