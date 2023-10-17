using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products.Command
{
    public class CreateProductCommand : ICommand<CreateProductCommandResult>
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
        public List<ProductColorInfo> ProductColors { get; set; }
        public List<ProductDetailInfo> ProductDetails { get; set; }

        public CreateProductCommand(
            string code,
            string name,
            int classificationId,
            int materialId,
            int supplierId,
            int trademarkId,
            int originId,
            string description,
            List<string> productImages,
            List<ProductColorInfo> productColors,
            List<ProductDetailInfo> productDetails
        )
        {
            Code = code;
            Name = name;
            ClassificationId = classificationId;
            MaterialId = materialId;
            SupplierId = supplierId;
            TrademarkId = trademarkId;
            OriginId = originId;
            Description = description;
            ProductImages = productImages;
            ProductColors = productColors;
            ProductDetails = productDetails;
        }
    }
}