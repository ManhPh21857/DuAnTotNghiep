using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Query;

namespace Project.Product.ApplicationService.Products.Query
{
    public class GetProductItemQueryHandler : QueryHandler<GetProductItemQuery, GetProductItemQueryResult>
    {
        private readonly IProductRepository productRepository;

        public GetProductItemQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async override Task<GetProductItemQueryResult> Handle(GetProductItemQuery request, CancellationToken cancellationToken)
        {
            var product = await this.productRepository.GetProductView(request.Id);

            var productDetail = await this.productRepository.GetProductDetail(request.Id);
            
            var productColor = await this.productRepository.GetProductColor(request.Id);

            var productSize = await this.productRepository.GetProductSize(request.Id);

            return new GetProductItemQueryResult(product, productDetail, productColor, productSize);
        }
    }
}
