using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Query;

namespace Project.Sales.ApplicationService.CartDetails.Query
{
    public class GetCartdetailQueryHandler : QueryHandler<GetCartdetailQuery, GetCartdetailQueryResult>
    {
        private readonly ICartdetailRepository cartService;

        public GetCartdetailQueryHandler(ICartdetailRepository cartService)
        {
            this.cartService = cartService;
        }

        public async override Task<GetCartdetailQueryResult> Handle(GetCartdetailQuery request, CancellationToken cancellationToken)
        {
            var result = await this.cartService.GetCartdetail();

            return new GetCartdetailQueryResult(result.ToList());
        }
    }
}
