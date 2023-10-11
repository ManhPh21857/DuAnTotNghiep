namespace Project.Common.Infrastructure.WebAPI.Controllers.v1.Products.Get;

public class GetProductResponseModel
{
    public List<ProductModel> Products { get; set; }

    public GetProductResponseModel()
    {
        Products = new List<ProductModel>();
    }
}