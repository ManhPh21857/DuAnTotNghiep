namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class GetSaleCounterResponseModel
    {
        public IEnumerable<GetSaleCounterModel> Salecounters { get; set; }

        public GetSaleCounterResponseModel(IEnumerable<GetSaleCounterModel> salecounters)
        {
            Salecounters = salecounters;
        }
    }
}
