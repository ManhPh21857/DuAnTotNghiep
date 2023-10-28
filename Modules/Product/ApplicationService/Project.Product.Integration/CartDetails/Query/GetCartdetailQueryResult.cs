
using Project.Product.Domain.CartDetails;

namespace Project.Product.Integration.CartDetails.Query
{
    public class GetCartdetailQueryResult 
    {
        public IList<CartdetailInfo> Cartdetails { get; set; }
        public GetCartdetailQueryResult(IList<CartdetailInfo> cartdetails)
        {
            Cartdetails = cartdetails;
        }
    }
}
