namespace Project.Sales.Domain.SaleCounters
{
    public interface ISaleCounterRepository
    {
        Task<IEnumerable<SaleCounterInfo>> GetSaleCounterView();
        Task<SaleCounterInfo> GetProductDetailId(int productId,int colorId, int sizeId);
        Task CreateOrderDetail(OrderDetailInfo orderDetail);
        Task<int> CreateOrder(OrderInfo order);
        Task<OrderDetailInfo> CheckOrderId(int id);
    }
}
