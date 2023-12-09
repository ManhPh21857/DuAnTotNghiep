using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Integration.SaleCounters.Query;

namespace Project.Sales.ApplicationService.SaleCounters.Query
{
    public class GetProductDetailIdQueryHandler : QueryHandler<GetProductDetailIdQuery, GetProductDetailIdQueryResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;

        public GetProductDetailIdQueryHandler(ISaleCounterRepository saleCounterRepository)
        {
            this.saleCounterRepository = saleCounterRepository;
        }


        public async override Task<GetProductDetailIdQueryResult> Handle(GetProductDetailIdQuery request, CancellationToken cancellationToken)
        {
            var result = await this.saleCounterRepository.GetProductDetailId(request.ProductId, request.ColorId, request.SizeId);

            return new GetProductDetailIdQueryResult(result);
        }
    }
}
