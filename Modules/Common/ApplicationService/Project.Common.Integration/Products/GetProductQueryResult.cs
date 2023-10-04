using Project.Common.Domain.Products;

namespace Project.Common.Integration.Products;

public class GetProductQueryResult
{
    public List<ProductInfo> Products { get; set; }

    public GetProductQueryResult(List<ProductInfo> products)
    {
        this.Products = products;
    }
}