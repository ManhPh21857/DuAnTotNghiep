namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get
{
    public class SaleCounterIdModel
    {
        public int ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
