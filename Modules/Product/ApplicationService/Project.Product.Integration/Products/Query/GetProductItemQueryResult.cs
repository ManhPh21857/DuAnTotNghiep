using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductItemQueryResult
    {
        public ProductView Product { get; set; }
        public IEnumerable<ProductDetailInfo> ProductDetails { get; set; }
        public IEnumerable<ProductColorInfo> ProductColors { get; set; }
        public IEnumerable<ProductSizeInfo> ProductSizes { get; set; }

        public GetProductItemQueryResult(
            ProductView product,
            IEnumerable<ProductDetailInfo> productDetails,
            IEnumerable<ProductColorInfo> productColors,
            IEnumerable<ProductSizeInfo> productSizes
        )
        {
            Product = product;
            ProductDetails = productDetails;
            ProductColors = productColors;
            ProductSizes = productSizes;
        }
    }
}