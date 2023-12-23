using Project.Sales.Domain.Orders;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetOrderDetailQueryResult
    {
        public IEnumerable<OrderDetailInfo> OrderDetails { get; set; }

        public GetOrderDetailQueryResult(IEnumerable<OrderDetailInfo> orderDetails)
        {
            this.OrderDetails = orderDetails;
        }
    }
}
