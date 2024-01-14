namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.Dashboarts.Get
{
    public class OrderUnconfimredModel
    {
        public Guid OrderCode { get; set; }
        public string FullName { get; set; }
        public float OrderTotal { get; set; }
        public int Status { get; set; }
    }
}
