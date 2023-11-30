namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class SaleCounterIdResponseModel
    {
        public SaleCounterIdModel Salecounters { get; set; }

        public SaleCounterIdResponseModel(SaleCounterIdModel salecounters)
        {
            Salecounters = salecounters;
        }

    }
}
