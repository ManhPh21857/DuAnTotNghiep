
using Project.Product.Domain.Products;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartdetailResponseModel
    {
        public IEnumerable<CartDetailModel> Cartdetails { get; set; }
        public IEnumerable<ProductColorModel> ProductColors { get; set; }
        public IEnumerable<ProductSizeModel> ProductSizes { get; set; }

        public CartdetailResponseModel(
            IEnumerable<CartDetailModel> cartdetails, 
            IEnumerable<ProductColorModel> productColors, 
            IEnumerable<ProductSizeModel> productSizes)
        {
            Cartdetails = cartdetails;
            ProductColors = productColors;
            ProductSizes = productSizes;
        }
    }
}
