
using Project.Product.Domain.Products;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartdetailResponseModel
    {
        public IEnumerable<CartDetailModel> Cartdetails { get; set; }

        public CartdetailResponseModel(
            IEnumerable<CartDetailModel> cartdetails)
        {
            Cartdetails = cartdetails;
        }
    }
}
