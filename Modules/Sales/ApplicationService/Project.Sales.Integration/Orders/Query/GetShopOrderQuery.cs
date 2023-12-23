using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Orders;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetShopOrderQuery : IQuery<GetShopOrderQueryResult>
    {
        public int Page { get; set; }
        public GetOrderFilter? Filter { get; set; }

        public GetShopOrderQuery(int page, GetOrderFilter? filter)
        {
            this.Page = page;
            this.Filter = filter;
        }
    }
}
