using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.SaleCounters.Query
{
    public class GetSaleCounterQuery : IQuery<GetSaleCounterQueryResult>
    {
        public int PageNo { get; set; }

        public GetSaleCounterQuery(int pageNo)
        {
            PageNo = pageNo;
        }
    }
}
