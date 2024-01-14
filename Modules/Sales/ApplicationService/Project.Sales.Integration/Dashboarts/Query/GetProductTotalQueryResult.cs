using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetProductTotalQueryResult
    {
        public IEnumerable<ProductTotalInfo> ProductTotals { get; set; }

        public GetProductTotalQueryResult(IEnumerable<ProductTotalInfo> productTotals)
        {
            ProductTotals = productTotals;
        }
    }
}
