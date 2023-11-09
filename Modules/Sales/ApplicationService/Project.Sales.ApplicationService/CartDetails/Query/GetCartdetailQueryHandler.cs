using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Products;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Query;

namespace Project.Sales.ApplicationService.CartDetails.Query
{
    public class GetCartdetailQueryHandler : QueryHandler<GetCartdetailQuery, GetCartdetailQueryResult>
    {
        private readonly ICartdetailRepository cartService;
        private readonly IProductRepository productRepository;

        public GetCartdetailQueryHandler(ICartdetailRepository cartService, IProductRepository productRepository)
        {
            this.cartService = cartService;
            this.productRepository = productRepository;
        }

        public async override Task<GetCartdetailQueryResult> Handle(GetCartdetailQuery request, CancellationToken cancellationToken)
        {
            int cartId = 1;

            var cartDetails = await this.cartService.GetCartdetail(cartId);

            var listProductId = cartDetails.Select(c => c.ProductId ?? 0).Distinct().Where(x => x != 0).ToList();

            return new GetCartdetailQueryResult(cartDetails);
        }
    }
}
