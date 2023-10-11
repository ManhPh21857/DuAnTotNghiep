using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Constants;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products;

namespace Project.Product.ApplicationService.Products;

public class GetProductQueryHandler : CommandHandler<GetProductQuery, GetProductQueryResult>
{
    private readonly IProductRepository productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    public async override Task<GetProductQueryResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        int skip = (request.PageNumber - 1) * CommonConst.PageNumber;

        int take = CommonConst.PageNumber;

        var result = await productRepository.GetProducts(skip,take);

        return new GetProductQueryResult(result);
    }
}