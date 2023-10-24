using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class UpdateProductRequestModel
    {
        public ProductModel Product { get; set; }
        public List<ProductColorModel> ProductColors { get; set; }
        public List<ProductSizeModel> ProductSizes { get; set; }
        public List<ProductDetailModel> ProductDetails { get; set; }
    }
}