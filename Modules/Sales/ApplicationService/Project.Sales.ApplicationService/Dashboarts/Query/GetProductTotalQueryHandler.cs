using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetProductTotalQueryHandler : QueryHandler<GetProductTotalQuery, GetProductTotalQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetProductTotalQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }

        public async override Task<GetProductTotalQueryResult> Handle(GetProductTotalQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.GetProductTotal();

            return new GetProductTotalQueryResult(result);
        }
    }
}
