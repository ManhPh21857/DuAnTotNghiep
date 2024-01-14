using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetOrderTotalQueryHandler : QueryHandler<GetOrderTotalQuery, GetOrderTotalQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetOrderTotalQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }

        public async override Task<GetOrderTotalQueryResult> Handle(GetOrderTotalQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.GetOrderTotal();

            return new GetOrderTotalQueryResult(result);
        }
    }
}
