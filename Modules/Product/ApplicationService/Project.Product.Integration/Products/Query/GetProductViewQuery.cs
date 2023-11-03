using Project.Core.ApplicationService.Queries;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductViewQuery : IQuery<GetProductViewQueryResult>
    {
        public int PageNo { get; set; }

        public GetProductViewQuery(int pageNo)
        {
            PageNo = pageNo;
        }
    }
}
