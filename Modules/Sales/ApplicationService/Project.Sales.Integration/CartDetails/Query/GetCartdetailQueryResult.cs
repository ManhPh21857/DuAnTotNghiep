using Project.Sales.Domain.CartDetails;

namespace Project.Sales.Integration.CartDetails.Query
{
    public class GetCartdetailQueryResult 
    {
        public IEnumerable<CartDetailInfo> Cartdetails { get; set; }

        public GetCartdetailQueryResult(
            IEnumerable<CartDetailInfo> cartdetails
        )
        {
            Cartdetails = cartdetails;
        }
    }
}
