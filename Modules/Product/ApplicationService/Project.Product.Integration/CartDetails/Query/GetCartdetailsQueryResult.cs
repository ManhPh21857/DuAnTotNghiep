using Project.Product.Domain.Products;
using Project.Product.Domain.CartDetails;
namespace Project.Product.Integration.CartDetails.Query
{
    public class GetCartdetailsQueryResult
    {
        public IList<CartdetailInfo> Cartdetails { get; set; }
        public GetCartdetailsQueryResult(IList<CartdetailInfo> cartdetails)
        {
            Cartdetails = cartdetails;
        }
    }
}
