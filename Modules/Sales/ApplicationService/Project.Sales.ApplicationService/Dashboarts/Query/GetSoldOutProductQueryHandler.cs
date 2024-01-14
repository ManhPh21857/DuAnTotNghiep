using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetSoldOutProductQueryHandler : QueryHandler<GetSoldOutProductQuery, GetSoldOutProductQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetSoldOutProductQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }

       

        public async override Task<GetSoldOutProductQueryResult> Handle(GetSoldOutProductQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.SoldOutProducts();

            return new GetSoldOutProductQueryResult(result);
        }
    }
}
