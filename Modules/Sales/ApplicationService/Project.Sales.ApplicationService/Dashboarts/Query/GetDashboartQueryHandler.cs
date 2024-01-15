using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetDashboartQueryHandler : QueryHandler<GetDashboartQuery, GetDashboartQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetDashboartQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }
     
        public async override Task<GetDashboartQueryResult> Handle(GetDashboartQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.GetCustomerTotal();
            
            return new GetDashboartQueryResult(result);
        }
    }
}
