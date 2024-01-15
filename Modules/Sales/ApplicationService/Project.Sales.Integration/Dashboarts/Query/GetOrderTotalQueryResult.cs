using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetOrderTotalQueryResult
    {
        public IEnumerable<OrderTotalInfo> OrderTotals { get; set; }

        public GetOrderTotalQueryResult(IEnumerable<OrderTotalInfo> orderTotals)
        {
            OrderTotals = orderTotals;
        }
    }
}
