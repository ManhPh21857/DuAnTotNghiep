namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class OrderDetailModel
    {
        public int ProductDetailId { get; set; }
        public int VoucherId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
