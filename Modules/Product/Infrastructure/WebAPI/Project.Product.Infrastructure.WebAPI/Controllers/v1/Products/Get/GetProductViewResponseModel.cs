namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get
{
    public class GetProductViewResponseModel
    {
        public IEnumerable<ProductView> Products { get; set; }
        public int TotalProduct { get; set; }

        public GetProductViewResponseModel()
        {
            Products = new List<ProductView>();
        }
    }
}