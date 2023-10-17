namespace Project.Product.Domain.Products;

public class ProductInfo
{
    public string Code { get; set; }
    public string Name { get; set; }
    public int ClassificationId { get; set; }
    public int MaterialId { get; set; }
    public int SupplierId { get; set; }
    public int TrademarkId { get; set; }
    public int OriginId { get; set; }
    public string Description { get; set; }

    public ProductInfo()
    {
        Code = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
    }
}