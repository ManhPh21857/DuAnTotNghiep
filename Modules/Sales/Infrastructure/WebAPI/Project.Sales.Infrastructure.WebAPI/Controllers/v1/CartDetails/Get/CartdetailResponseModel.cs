
namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartdetailResponseModel
    {
        public IEnumerable<CartDetailModel> Cartdetails { get; set; }
        public CartdetailResponseModel()
        {
            Cartdetails = new List<CartDetailModel>();
        }
    }
}
