namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get
{
    public class ProductDetail
    {
        public int? Id { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
