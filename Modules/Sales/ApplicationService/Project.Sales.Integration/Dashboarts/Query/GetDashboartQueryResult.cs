using Project.Sales.Domain.Dashboards;

namespace Project.Sales.Integration.Dashboarts.Query
{
    public class GetDashboartQueryResult
    {
        public IEnumerable<DashboardInfo> Dashboards { get; set; }

        public GetDashboartQueryResult(IEnumerable<DashboardInfo> dashboards)
        {
            Dashboards = dashboards;
        }
    }
}
