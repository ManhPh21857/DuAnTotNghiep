using Project.Sales.Domain.Dashboards;
namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetOrderUnConfimredQueryResult
    {
        public IEnumerable<OrderUnconfimred> OrderUnConfimreds { get; set; }

        public GetOrderUnConfimredQueryResult(IEnumerable<OrderUnconfimred> orderUnConfimreds)
        {
            OrderUnConfimreds = orderUnConfimreds;
        }
    }
}
