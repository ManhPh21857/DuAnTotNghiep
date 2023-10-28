using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Command
{
    public class CreateProductCommand : ICommand<CreateProductCommandResult>
    {
        public ProductInfo Product { get; set; }
        public List<ProductColorInfo> ProductColors { get; set; }
        public List<ProductSizeInfo> ProductSizes { get; set; }
        public List<ProductDetailInfo> ProductDetails { get; set; }

        public CreateProductCommand(
            ProductInfo product,
            List<ProductColorInfo> productColors,
            List<ProductSizeInfo> productSizes,
            List<ProductDetailInfo> productDetails
        )
        {
            Product = product;
            ProductColors = productColors;
            ProductSizes = productSizes;
            ProductDetails = productDetails;
        }
    }
}