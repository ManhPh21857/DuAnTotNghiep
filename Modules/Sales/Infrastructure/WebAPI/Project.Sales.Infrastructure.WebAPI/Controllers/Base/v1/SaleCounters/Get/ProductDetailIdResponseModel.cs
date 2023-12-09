namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class ProductDetailIdResponseModel
    {
        public ProductDetailIdModel Salecounters { get; set; }

        public ProductDetailIdResponseModel(ProductDetailIdModel salecounters)
        {
            Salecounters = salecounters;
        }

    }
}
