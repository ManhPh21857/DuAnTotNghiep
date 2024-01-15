namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class ProductTotalResponseModel
    {
        public IEnumerable<ProductTotalModel> ProductTotals { get; set; }

        public ProductTotalResponseModel(IEnumerable<ProductTotalModel> productTotals)
        {
            ProductTotals = productTotals;
        }
    }
}
