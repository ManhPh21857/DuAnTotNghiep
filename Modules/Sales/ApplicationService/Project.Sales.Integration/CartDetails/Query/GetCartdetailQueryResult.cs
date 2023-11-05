
using Project.Sales.Domain.CartDetails;

namespace Project.Sales.Integration.CartDetails.Query
{
    public class GetCartdetailQueryResult 
    {
        public IList<CartDetailInfo> Cartdetails { get; set; }
        public GetCartdetailQueryResult(IList<CartDetailInfo> cartdetails)
        {
            Cartdetails = cartdetails;
        }
    }
}
