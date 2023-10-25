using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Query;

namespace Project.Product.ApplicationService.Products.Query
{
    public class GetProductDetailQueryHandler : QueryHandler<GetProductDetailQuery, GetProductDetailQueryResult>
    {
        private readonly IProductRepository productRepository;

        public GetProductDetailQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async override Task<GetProductDetailQueryResult> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProduct(request.Id);

            var productColor = await productRepository.GetProductColor(request.Id);

            var productSize = await productRepository.GetProductSize(request.Id);

            var productDetail = await productRepository.GetProductDetail(request.Id);

            var result = new GetProductDetailQueryResult(
                product,
                productColor,
                productSize,
                productDetail
            );

            return result;
        }
    }
}
