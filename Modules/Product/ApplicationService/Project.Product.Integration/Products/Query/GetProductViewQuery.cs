using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductViewQuery : IQuery<GetProductViewQueryResult>
    {
        public int PageNo { get; set; }
        public GetProductFilterParam Filter { get; set; }

        public GetProductViewQuery(int pageNo, GetProductFilterParam filter)
        {
            this.PageNo = pageNo;
            this.Filter = filter;
        }
    }
}
