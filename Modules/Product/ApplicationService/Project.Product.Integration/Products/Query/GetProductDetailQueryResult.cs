using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductDetailQueryResult
    {
        public ProductInfo Product { get; set; }
        public IEnumerable<ProductColorInfo> ProductColors { get; set; }
        public IEnumerable<ProductSizeInfo> ProductSizes { get; set; }
        public IEnumerable<ProductDetailInfo> ProductDetails { get; set; }

        public GetProductDetailQueryResult(
            ProductInfo product,
            IEnumerable<ProductColorInfo> productColors,
            IEnumerable<ProductSizeInfo> productSizes,
            IEnumerable<ProductDetailInfo> productDetails
        )
        {
            Product = product;
            ProductColors = productColors;
            ProductSizes = productSizes;
            ProductDetails = productDetails;
        }
    }
}