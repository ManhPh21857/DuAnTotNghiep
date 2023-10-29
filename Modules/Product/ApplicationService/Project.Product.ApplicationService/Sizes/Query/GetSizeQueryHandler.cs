using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Sizes;
using Project.Product.Integration.Sizes.Query;

namespace Project.Product.ApplicationService.Sizes.Query
{
    public class GetSizeQueryHandler : QueryHandler<GetSizeQuery, GetSizeQueryResult>
    {
        private readonly ISizeRepository sizeRepository;

        public GetSizeQueryHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public async override Task<GetSizeQueryResult> Handle(GetSizeQuery request, CancellationToken cancellationToken)
        {
            var result = await sizeRepository.GetSizes(null);

            return new GetSizeQueryResult(result.ToList());
        }
    }
}
