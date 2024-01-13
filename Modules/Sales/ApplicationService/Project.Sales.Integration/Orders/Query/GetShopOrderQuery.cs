using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.Orders.Query
{
    public class GetShopOrderQuery : IQuery<GetShopOrderQueryResult>
    {
        public int Page { get; set; }
        public int Model { get; set; }

        public GetShopOrderQuery(int page, int model)
        {
            this.Page = page;
            this.Model = model;
        }
    }
}
