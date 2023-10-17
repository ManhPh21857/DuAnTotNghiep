namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class CreateProductRequestModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int ClassificationId { get; set; }
        public int MaterialId { get; set; }
        public int SupplierId { get; set; }
        public int TrademarkId { get; set; }
        public int OriginId { get; set; }
        public string Description { get; set; }
        public List<string> ProductImages { get; set; }
        public List<ProductColorModel> ProductColors { get; set; }
        public List<ProductDetailModel> ProductDetails { get; set; }
    }
}