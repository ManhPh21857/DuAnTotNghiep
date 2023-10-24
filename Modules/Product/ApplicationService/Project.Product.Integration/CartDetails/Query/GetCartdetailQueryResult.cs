
using Project.Product.Domain.CartDetails;

namespace Project.Product.Integration.CartDetails.Query
{
    public class GetCartdetailQueryResult 
    {
        public IList<GetCartdetailInfo> Cartdetail { get; set; }
        public GetCartdetailQueryResult(IList<GetCartdetailInfo> cartdetail)
        {
            Cartdetail = cartdetail;
        }
    }
}
