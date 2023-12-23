namespace Project.Sales.Domain.Orders
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(CreateOrderParam param);
        Task CreateOrderDetail(CreateOrderDetailParam param);
        Task FinishOrderOnlinePayment(FinishOrderOnlinePaymentParam param);
        Task<IEnumerable<OrderInfo>> GetOrders(int customerId, int? order);
        Task<OrderInfo> GetOrder(int id);
        Task<IEnumerable<OrderDetailInfo>> GetOrderDetails(int orderId);
        Task CancelOrder(int id, int customerId);
        Task<(IEnumerable<OrderInfo>, int)> GetShopOrder(int skip, int take, GetOrderFilter? param);
        Task AssignEmployee(int id, int employeeId, byte[]? dateVersion);
        Task UpdateOrderStatus(int id, int status, byte[]? dateVersion);
        Task<int?> GetOrderAssign(int? id);
    }
}
