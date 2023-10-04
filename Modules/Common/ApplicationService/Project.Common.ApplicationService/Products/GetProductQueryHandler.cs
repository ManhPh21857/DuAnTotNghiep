using Project.Common.Domain.Constants;
using Project.Common.Domain.Products;
using Project.Common.Integration.Products;
using Project.Core.ApplicationService.Commands;

namespace Project.Common.ApplicationService.Products;

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