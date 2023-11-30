namespace Project.Sales.Domain.SaleCounters
{
    public class SaleCounterInfo
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductDetailId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public byte[]? DataVersion { get; set; }
        public int Quantity { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public float AvgPrice { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }

    }
}
