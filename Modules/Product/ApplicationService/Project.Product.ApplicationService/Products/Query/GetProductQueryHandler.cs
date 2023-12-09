using Project.Core.ApplicationService.Queries;
using Project.Core.Domain.Constants;
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
            int skip = (request.PageNo - 1) * CommonConst.PRODUCT_PAGE_SIZE;
            int take = CommonConst.PRODUCT_PAGE_SIZE;

            var result = await this.productRepository.GetProducts(skip, take);

            int totalPage = result.TotalProduct / CommonConst.PRODUCT_PAGE_SIZE;
            if (result.TotalProduct % CommonConst.PRODUCT_PAGE_SIZE > 0)
            {
                totalPage++;
            }

            return new GetProductQueryResult(result.Products, totalPage);
        }
    }
}
