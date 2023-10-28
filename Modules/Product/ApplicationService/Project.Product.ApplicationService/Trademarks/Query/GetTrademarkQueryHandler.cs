using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Trademarks;
using Project.Product.Integration.Trademarks.Query;

namespace Project.Product.ApplicationService.Trademarks.Query
{
    public class GetTrademarkQueryHandler : QueryHandler<GetTrademarkQuery, GetTrademarkQueryResult>
    {
        private readonly ITrademarkRepository Trademark;

        public GetTrademarkQueryHandler(ITrademarkRepository trademark)
        {
            this.Trademark = trademark;
        }

        public async override Task<GetTrademarkQueryResult> Handle(GetTrademarkQuery request, CancellationToken cancellationToken)
        {
            var result = await Trademark.GetTrademark(null);

            return new GetTrademarkQueryResult(result.ToList());
        }
    }
}
