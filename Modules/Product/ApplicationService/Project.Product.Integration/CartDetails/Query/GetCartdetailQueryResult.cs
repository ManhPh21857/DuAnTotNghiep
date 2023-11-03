
using Project.Product.Domain.CartDetails;

namespace Project.Product.Integration.CartDetails.Query
{
    public class GetCartdetailQueryResult 
    {
        public List<CartDetailView> CartDetails { get; set; }
        public GetCartdetailQueryResult(List<CartDetailView> cartdetails)
        {
            CartDetails = cartdetails;
        }
    }
}
