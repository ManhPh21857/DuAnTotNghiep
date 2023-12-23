using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetOrderDetailQuery : IQuery<GetOrderDetailQueryResult>
    {
        public int OrderId { get; set; }

        public GetOrderDetailQuery(int orderId)
        {
            this.OrderId = orderId;
        }
    }
}
