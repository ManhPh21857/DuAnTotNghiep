using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Constants;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Query;

namespace Project.Product.ApplicationService.Products.Query
{
    public class GetProductQueryHandler : QueryHandler<GetProductQuery, GetProductQueryResult>
    {
        private readonly IProductRepository productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async override Task<GetProductQueryResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            int skip = (request.PageNo - 1) * CommonConst.PageSize;
            int take = CommonConst.PageSize;

            var result = (await this.productRepository.GetProducts(skip, take)).ToList();

            return new GetProductQueryResult(result);
        }
    }
}
