
namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartdetailResponseModel
    {
        public IEnumerable<CartdetailModel> Cartdetails { get; set; }

        public CartdetailResponseModel()
        {
            Cartdetails = new List<CartdetailModel>();
        }
    }
}
