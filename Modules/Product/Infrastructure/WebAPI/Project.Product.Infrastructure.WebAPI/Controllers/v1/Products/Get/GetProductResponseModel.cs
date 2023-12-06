namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get;

public class GetProductResponseModel
{
    public List<ProductViewModel> Products { get; set; }
    public int TotalPage { get; set; }

    public GetProductResponseModel()
    {
        this.Products = new List<ProductViewModel>();
    }
}