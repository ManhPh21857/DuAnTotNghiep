using Project.Core.ApplicationService.Queries;

namespace Project.Product.Integration.Products.Query
{
    public class GetProductItemQuery : IQuery<GetProductItemQueryResult>
    {
        public int Id { get; set; }

        public GetProductItemQuery(int id)
        {
            Id = id;
        }
    }
}
