namespace Project.Sales.Domain.SaleCounters
{
    public interface ISaleCounterRepository
    {
        Task<IEnumerable<SaleCounterInfo>> GetSaleCounterView();
        Task<SaleCounterInfo> GetProductDetailId(int productId,int colorId, int sizeId);
        Task<int> GetQuantity(int productId, int colorId, int sizeId);
        Task CreateOrderDetail(OrderDetailInfo param);
        Task<int> CreateOrder(OrderInfo param);
        Task<OrderDetailInfo> CheckOrderId(int id);
        Task UpdateQuantity(UpdateQuantityInfo sale);
    }
}
