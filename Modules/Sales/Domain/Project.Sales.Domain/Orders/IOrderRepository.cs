namespace Project.Sales.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(CreateOrderParam param);
        Task CreateOrderDetail(CreateOrderDetailParam param);
        Task FinishOrderOnlinePayment(FinishOrderOnlinePaymentParam param);
        Task<IEnumerable<OrderInfo>> GetOrders(int customerId);
        Task<IEnumerable<OrderDetailInfo>> GetOrderDetails(int orderId);
    }
}
