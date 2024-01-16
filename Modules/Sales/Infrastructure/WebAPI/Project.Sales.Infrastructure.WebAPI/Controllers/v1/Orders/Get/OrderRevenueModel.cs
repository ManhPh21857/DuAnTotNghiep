namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class OrderRevenueModel
    {
        public int Id { get; set; }
        public float OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
