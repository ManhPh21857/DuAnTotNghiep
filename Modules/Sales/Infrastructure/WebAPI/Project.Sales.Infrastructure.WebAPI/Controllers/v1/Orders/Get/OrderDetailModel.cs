namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class OrderDetailModel
    {
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
