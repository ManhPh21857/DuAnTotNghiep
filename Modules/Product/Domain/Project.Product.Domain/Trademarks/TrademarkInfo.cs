namespace Project.Product.Domain.Trademarks
{
    public class TrademarkInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? IsDeleted { get; set; }
        public byte[]? DataVersion { get; set; }
    }
}
