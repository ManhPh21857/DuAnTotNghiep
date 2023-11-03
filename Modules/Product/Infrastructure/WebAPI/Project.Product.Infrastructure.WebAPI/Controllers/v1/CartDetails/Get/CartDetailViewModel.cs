
namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartDetailViewModel
    {
        public int? ProductDetailId { get; set; }
        public int? CartId { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
