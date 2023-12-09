using Project.Sales.Domain.SaleCounters;

namespace Project.Sales.Integration.SaleCounters.Query
{
    public class GetProductDetailIdQueryResult
    {
        public SaleCounterInfo Salecounters { get; set; }

        public GetProductDetailIdQueryResult(SaleCounterInfo saleCounters)
        {
            Salecounters = saleCounters;
        }
    }
}
