using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.Sales.Domain.Carts;
using Project.Sales.Integration.Carts.Query;

namespace Project.Sales.ApplicationService.Carts.Query
{
    public class GetCartDetailQueryHandler : QueryHandler<GetCartDetailQuery, GetCartDetailQueryResult>
    {
        private readonly ICartRepository cartRepository;
        private readonly ISessionInfo sessionInfo;

        public GetCartDetailQueryHandler(ICartRepository cartRepository, ISessionInfo sessionInfo)
        {
            this.cartRepository = cartRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<GetCartDetailQueryResult> Handle(
            GetCartDetailQuery request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.Value;
            int? cartId = await this.cartRepository.FindCartId(userId);
            if (!cartId.HasValue)
            {
                return new GetCartDetailQueryResult(new List<CartDetailInfo>());
            }

            var cartDetails = await this.cartRepository.GetCartDetail(cartId.Value);

            return new GetCartDetailQueryResult(cartDetails);
        }
    }
}
