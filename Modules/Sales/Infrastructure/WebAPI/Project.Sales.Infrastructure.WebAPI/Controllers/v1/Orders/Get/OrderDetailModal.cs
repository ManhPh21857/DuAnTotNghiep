namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get
{
    public class OrderDetailModal
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
