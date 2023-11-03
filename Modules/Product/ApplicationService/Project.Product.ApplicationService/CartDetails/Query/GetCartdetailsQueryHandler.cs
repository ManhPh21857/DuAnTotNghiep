using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.CartDetails;
using Project.Product.Integration.CartDetails.Query;

namespace Project.Product.ApplicationService.CartDetails.Query
{
    public class GetCartdetailsQueryHandler : QueryHandler<GetCartdetailsQuery, GetCartdetailsQueryResult>
    {
        private readonly ICartdetailRepository cartdetailRepository;
        public GetCartdetailsQueryHandler(ICartdetailRepository cartdetailRepository)
        {
            this.cartdetailRepository = cartdetailRepository;
        }


        public override async Task<GetCartdetailsQueryResult> Handle(GetCartdetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await cartdetailRepository.GetCartdetai();
            return new GetCartdetailsQueryResult(result.ToList());

            
        }
    }
}
