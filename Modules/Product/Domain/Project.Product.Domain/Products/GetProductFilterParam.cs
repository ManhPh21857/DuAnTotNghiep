namespace Project.Product.Domain.Products
{
    public class GetProductFilterParam
    {
        public string? Name { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public List<int>? ColorIds { get; set; }
        public List<int>? SizeIds { get; set; }
        public List<int>? SupplierIds { get; set; }
        public List<int>? MaterialIds { get; set; }
        public List<int>? ClassificationIds { get; set; }
        public List<int>? OriginIds { get; set; }
        public List<int>? TrademarkIds { get; set; }
    }
}
