namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Get
{
    public class CartDetailResponseModel
    {
        public IEnumerable<CartDetailModel> CartDetails { get; set; }

        public CartDetailResponseModel(IEnumerable<CartDetailModel> cartDetails)
        {
            this.CartDetails = cartDetails;
        }
    }
}
