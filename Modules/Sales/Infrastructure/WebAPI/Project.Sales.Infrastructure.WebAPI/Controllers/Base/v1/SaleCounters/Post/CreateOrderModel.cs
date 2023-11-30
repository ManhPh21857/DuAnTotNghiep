namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class CreateOrderModel
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public int AddressId { get; set; }
        public float Total { get; set; }
    }
}
