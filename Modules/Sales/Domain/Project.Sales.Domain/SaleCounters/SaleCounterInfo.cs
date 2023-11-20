namespace Project.Sales.Domain.SaleCounters
{
    public class SaleCounterInfo
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte[]? DataVersion { get; set; }
        public int Quantity { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public float AvgPrice { get; set; }
        public string Description { get; set; }
    }
}
