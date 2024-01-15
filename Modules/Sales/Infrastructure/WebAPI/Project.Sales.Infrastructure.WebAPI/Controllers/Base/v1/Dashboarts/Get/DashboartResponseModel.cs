namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class DashboartResponseModel
    {
        public IEnumerable<DashboartModel> Dashboards { get; set; }

        public DashboartResponseModel(IEnumerable<DashboartModel> dashboards)
        {
            Dashboards = dashboards;
        }
    }
}
