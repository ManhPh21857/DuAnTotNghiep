namespace Project.Sales.Domain.CartDetails
{
    public class CartDetailInfo
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
