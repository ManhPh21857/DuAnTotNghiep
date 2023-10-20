namespace Project.Common.Infrastructure.WebAPI.Controllers.v1.Products.Get;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public byte[]? DataVersion { get; set; }
    public int Quantity { get; set; }
    public float AvgPrice { get; set; }
    public string ClassificationName { get; set; }
    public string MaterialName { get; set; }
}