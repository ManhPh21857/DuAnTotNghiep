namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get
{
    public class GetProductItemResponseModel
    {
        public ProductView Product { get; set; }
        public IEnumerable<ProductDetail> ProductDetails { get; set; }
        public IEnumerable<ProductColorModel> ProductColors { get; set; }
        public IEnumerable<ProductSizeModel> ProductSizes { get; set; }

        public GetProductItemResponseModel(
            ProductView product,
            IEnumerable<ProductDetail> productDetails,
            IEnumerable<ProductColorModel> productColors,
            IEnumerable<ProductSizeModel> productSizes
        )
        {
            Product = product;
            ProductDetails = productDetails;
            ProductColors = productColors;
            ProductSizes = productSizes;
        }
    }
}