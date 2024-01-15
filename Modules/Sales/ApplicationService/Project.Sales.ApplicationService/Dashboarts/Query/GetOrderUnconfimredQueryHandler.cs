using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetOrderUnconfimredQueryHandler : QueryHandler<GetOrderUnConfimredQuery, GetOrderUnConfimredQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetOrderUnconfimredQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }



        public async override Task<GetOrderUnConfimredQueryResult> Handle(GetOrderUnConfimredQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.GetOrderUnconfimred();

            return new GetOrderUnConfimredQueryResult(result);
        }
    }
}
