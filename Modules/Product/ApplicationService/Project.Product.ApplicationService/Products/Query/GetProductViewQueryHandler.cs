using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Constants;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Query;

namespace Project.Product.ApplicationService.Products.Query
{
    public class GetProductViewQueryHandler : QueryHandler<GetProductViewQuery, GetProductViewQueryResult>
    {
        private readonly IProductRepository productRepository;

        public GetProductViewQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async override Task<GetProductViewQueryResult> Handle(GetProductViewQuery request, CancellationToken cancellationToken)
        {
            int skip = CommonConst.PageSize * (request.PageNo - 1);
            int take = CommonConst.PageSize;

            var result = await this.productRepository.GetProductView(skip, take);

            return new GetProductViewQueryResult(result.Products, result.TotalProduct);
        }
    }
}
