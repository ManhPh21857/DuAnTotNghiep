using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Constants;
using Project.Sales.Domain.SaleCounters;
using Project.Sales.Integration.SaleCounters.Query;

namespace Project.Sales.ApplicationService.SaleCounters.Query
{
    public class GetSaleCounterQueryHandler : QueryHandler<GetSaleCounterQuery, GetSaleCounterQueryResult>
    {
        private readonly ISaleCounterRepository saleCounterRepository;

        public GetSaleCounterQueryHandler(ISaleCounterRepository saleCounterRepository)
        {
            this.saleCounterRepository = saleCounterRepository;
        }

        
        public async override Task<GetSaleCounterQueryResult> Handle(GetSaleCounterQuery request, CancellationToken cancellationToken)
        {
            int skip = CommonConst.PageSize * (request.PageNo - 1);
            int take = CommonConst.PageSize;

            var result = await this.saleCounterRepository.GetSaleCounterView(skip, take);

            return new GetSaleCounterQueryResult(result.Salecounters, result.TotalProduct);
        }
    }
}
