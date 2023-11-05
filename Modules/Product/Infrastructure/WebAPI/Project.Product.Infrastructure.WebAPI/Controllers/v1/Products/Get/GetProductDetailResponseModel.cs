namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get
{
    public class GetProductDetailResponseModel
    {
        public ProductModel Product { get; set; }
        public IEnumerable<ProductColorModel> ProductColors { get; set; }
        public IEnumerable<ProductSizeModel> ProductSizes { get; set; }
        public IEnumerable<ProductDetailModel> ProductDetails { get; set; }
    }
}
