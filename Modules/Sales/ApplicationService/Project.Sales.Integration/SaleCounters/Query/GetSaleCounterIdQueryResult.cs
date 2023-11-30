using Project.Sales.Domain.SaleCounters;

namespace Project.Sales.Integration.SaleCounters.Query
{
    public class GetSaleCounterIdQueryResult
    {
        public SaleCounterInfo Salecounters { get; set; }

        public GetSaleCounterIdQueryResult(SaleCounterInfo saleCounters)
        {
            Salecounters = saleCounters;
        }
    }
}
