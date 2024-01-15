using Project.Core.ApplicationService.Queries;
using Project.Sales.Domain.Dashboards;
using Project.Sales.Integration.Dashboarts.Query;

namespace Project.Sales.ApplicationService.Dashboarts.Query
{
    public class GetSoldOutProductDetailQueryHandler : QueryHandler<GetSoldOutProductDetailQuery, GetSoldOutProductDetailQueryResult>
    {
        private readonly IDashboardsRepository dashboardsRepository;

        public GetSoldOutProductDetailQueryHandler(IDashboardsRepository dashboardsRepository)
        {
            this.dashboardsRepository = dashboardsRepository;
        }



        public async override Task<GetSoldOutProductDetailQueryResult> Handle(GetSoldOutProductDetailQuery request, CancellationToken cancellationToken)
        {
            var result = await this.dashboardsRepository.GetSoldOutProductDetail();

            return new GetSoldOutProductDetailQueryResult(result);
        }
    }
}
