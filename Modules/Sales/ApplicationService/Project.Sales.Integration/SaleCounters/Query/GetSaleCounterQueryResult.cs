using Project.Sales.Domain.SaleCounters;

namespace Project.Sales.Integration.SaleCounters.Query
{
    public class GetSaleCounterQueryResult
    {
        public IEnumerable<SaleCounterInfo> Salecounters { get; set; }

        public GetSaleCounterQueryResult(IEnumerable<SaleCounterInfo> saleCounters)
        {
            Salecounters = saleCounters;
        }
    }
}
