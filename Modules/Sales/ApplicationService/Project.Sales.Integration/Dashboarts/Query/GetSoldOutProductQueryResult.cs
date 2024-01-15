using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetSoldOutProductQueryResult
    {
        public IEnumerable<SoldOutProductInfo> SoldOutProducts { get; set; }

        public GetSoldOutProductQueryResult(IEnumerable<SoldOutProductInfo> soldOutProducts)
        {
            SoldOutProducts = soldOutProducts;
        }
    }
}
