using Project.Sales.Domain.Carts;

namespace Project.Sales.Integration.Carts.Query
{
    public class GetCartDetailQueryResult
    {
        public IEnumerable<CartDetailInfo> CartDetails { get; set; }

        public GetCartDetailQueryResult(IEnumerable<CartDetailInfo> cartDetails)
        {
            this.CartDetails = cartDetails;
        }
    }
}
