using Project.Product.Domain.Products;

namespace Project.Common.Infrastructure.WebAPI.Controllers.v1.Products.Get;

public class GetProductResponseModel
{
    public List<ProductViewModel> Products { get; set; }
}