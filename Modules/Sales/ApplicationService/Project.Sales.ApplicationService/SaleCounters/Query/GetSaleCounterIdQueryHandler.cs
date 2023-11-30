using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Integration.SaleCounters.Query;

namespace Project.Sales.ApplicationService.SaleCounters.Query
{
    public class GetSaleCounterIdQueryHandler : QueryHandler<GetSaleCounterIdQuery, GetSaleCounterIdQueryResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;

        public GetSaleCounterIdQueryHandler(ISaleCounterRepository saleCounterRepository)
        {
            this.saleCounterRepository = saleCounterRepository;
        }


        public async override Task<GetSaleCounterIdQueryResult> Handle(GetSaleCounterIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this.saleCounterRepository.GetSaleCounterViewId(request.ProductId, request.ColorId, request.SizeId);

            return new GetSaleCounterIdQueryResult(result);
        }
    }
}
