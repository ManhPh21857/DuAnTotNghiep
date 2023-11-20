namespace Project.Sales.Domain.SaleCounters
{
    public class SaleCounterResponse
    {
        public IEnumerable<SaleCounterInfo> Salecounters { get; set; }
        public int TotalProduct { get; set; }
    }
}
