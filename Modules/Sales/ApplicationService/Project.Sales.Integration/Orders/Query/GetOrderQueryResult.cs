using Project.Sales.Domain.Orders;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetOrderQueryResult
    {
        public IEnumerable<OrderInfo> Orders { get; set; }

        public GetOrderQueryResult(IEnumerable<OrderInfo> orders)
        {
            this.Orders = orders;
        }
    }
}
