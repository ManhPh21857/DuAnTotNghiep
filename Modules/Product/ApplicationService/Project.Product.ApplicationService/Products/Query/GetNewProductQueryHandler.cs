using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Query;

namespace Project.Product.ApplicationService.Products.Query
{
    public class GetNewProductQueryHandler : QueryHandler<GetNewProductQuery, GetNewProductQueryResult>
    {
        private readonly IProductRepository productRepository;

        public GetNewProductQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async override Task<GetNewProductQueryResult> Handle(
            GetNewProductQuery request,
            CancellationToken cancellationToken
        )
        {
            int skip = 0;
            int take = 5;

            var result = await this.productRepository.GetProductView(skip, take, new GetProductFilterParam());

            return new GetNewProductQueryResult(result.Products);
        }
    }
}
