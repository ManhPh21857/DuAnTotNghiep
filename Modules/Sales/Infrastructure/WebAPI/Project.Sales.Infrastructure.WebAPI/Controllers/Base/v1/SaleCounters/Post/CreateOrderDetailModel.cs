namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class CreateOrderDetailModel
    {
        
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int VoucherId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
