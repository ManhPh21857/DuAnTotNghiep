using Project.Core.ApplicationService.Queries;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductDetailQuery : IQuery<GetProductDetailQueryResult>
    {
        public int Id { get; set; }

        public GetProductDetailQuery(int id)
        {
            Id = id;
        }
    }
}
