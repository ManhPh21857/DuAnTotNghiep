namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class GetSaleCounterResponseModel
    {
        public IEnumerable<SaleCounterInfo> Salecounters { get; set; }
        public int TotalProduct { get; set; }

        public GetSaleCounterResponseModel()
        {
            Salecounters = new List<SaleCounterInfo>();
        }
    }
}
