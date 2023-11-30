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
           

            var result = await this.saleCounterRepository.GetSaleCounterView();

            return new GetSaleCounterQueryResult(result);
        }
    }
}
