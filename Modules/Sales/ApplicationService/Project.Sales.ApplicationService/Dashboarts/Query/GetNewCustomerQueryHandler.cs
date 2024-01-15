using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetNewCustomerQueryHandler : QueryHandler<GetNewCustomerQuery, GetNewCustomerQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetNewCustomerQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }
       
        public async override Task<GetNewCustomerQueryResult> Handle(GetNewCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.GetNewCustomer();

            return new GetNewCustomerQueryResult(result);
        }
    }
}
