namespace Project.Product.Domain.Products;

public class ProductInfo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public float MinPrice { get; set; }
    public float MaxPrice { get; set; }
    public int Evaluate { get; set; }
}