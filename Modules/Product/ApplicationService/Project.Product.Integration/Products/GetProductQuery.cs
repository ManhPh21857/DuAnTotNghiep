using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Products;

public class GetProductQuery : ICommand<GetProductQueryResult>
{
    public int PageNumber { get; set; }

    public GetProductQuery(int pageNumber)
    {
        this.PageNumber = pageNumber;
    }
}