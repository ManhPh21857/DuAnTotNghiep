using Project.Product.Domain.Products;

namespace Project.Product.Integration.Products;

public class GetProductQueryResult
{
    public List<ProductInfo> Products { get; set; }

    public GetProductQueryResult(List<ProductInfo> products)
    {
        this.Products = products;
    }
}