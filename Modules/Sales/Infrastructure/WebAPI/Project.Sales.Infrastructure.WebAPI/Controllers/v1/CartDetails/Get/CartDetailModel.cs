
namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Get
{
    public class CartDetailModel
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public string? Image { get; set; }
        public string? ProductName { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public byte[]? DataVersion { get; set; }


    }
}
