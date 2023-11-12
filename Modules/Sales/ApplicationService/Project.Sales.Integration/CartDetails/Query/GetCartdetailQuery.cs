using Project.Core.ApplicationService.Queries;

namespace Project.Sales.Integration.CartDetails.Query
{
    public class GetCartdetailQuery : IQuery<GetCartdetailQueryResult>
    {
        public int CartId { get; set; }
        public GetCartdetailQuery(int cartId)
        {
            CartId = cartId;
        }
    }
}
