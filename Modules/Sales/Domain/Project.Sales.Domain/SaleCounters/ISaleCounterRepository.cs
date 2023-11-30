namespace Project.Sales.Domain.SaleCounters
{
    public interface ISaleCounterRepository
    {
        Task<IEnumerable<SaleCounterInfo>> GetSaleCounterView();
        Task<SaleCounterInfo> GetSaleCounterViewId(int productId,int colorId, int sizeId);
        Task CreateOrderDetail(OrderDetailInfo orderDetail);
        Task CreateOrder(OrderDetailInfo orderDetail);
        Task<OrderDetailInfo> CheckOrderId(int orderId);
    }
}
