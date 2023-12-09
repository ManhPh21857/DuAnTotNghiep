using Project.Core.ApplicationService.Commands;
using Project.Core.ApplicationService.Queries;
using Project.Core.Domain;
using Project.Sales.Domain.Carts;
using Project.Sales.Integration.Carts.Query;

namespace Project.Sales.ApplicationService.Carts.Query
{
    public class GetCountItemQueryHandler : QueryHandler<GetCountItemQuery, GetCountItemQueryResult>
    {
        private readonly ICartRepository cartRepository;
        private readonly ISessionInfo sessionInfo;

        public GetCountItemQueryHandler(ICartRepository cartRepository, ISessionInfo sessionInfo)
        {
            this.cartRepository = cartRepository;
            this.sessionInfo = sessionInfo;
        }
        public async override Task<GetCountItemQueryResult> Handle(GetCountItemQuery request, CancellationToken cancellationToken)
        {
            int userId = this.sessionInfo.UserId.value;

            int count = await this.cartRepository.GetCountItem(userId);

            return new GetCountItemQueryResult(count);
        }
    }
}
