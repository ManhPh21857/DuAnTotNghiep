using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetSoldOutProductDetailQueryResult
    {
        public IEnumerable<SoldOutProductDetailInfo> SoldOutProductDetails { get; set; }

        public GetSoldOutProductDetailQueryResult(IEnumerable<SoldOutProductDetailInfo> soldOutProductDetails)
        {
            SoldOutProductDetails = soldOutProductDetails;
        }
    }
}
