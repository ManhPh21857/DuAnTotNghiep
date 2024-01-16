using Project.Sales.Domain.Orders;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetOrderRevenueQueryResult
    {
        public IEnumerable<OrderRevenueInfo> OrderRevenues { get; set; }

        public GetOrderRevenueQueryResult(IEnumerable<OrderRevenueInfo> orderRevenues)
        {
            this.OrderRevenues = orderRevenues;
        }
    }
}
