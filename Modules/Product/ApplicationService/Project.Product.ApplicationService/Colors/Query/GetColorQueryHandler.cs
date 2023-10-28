using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Colors;
using Project.Product.Integration.Colors.Query;

namespace Project.Product.ApplicationService.Colors.Query
{
    public class GetColorQueryHandler : QueryHandler<GetColorQuery, GetColorQueryResult>
    {
        private readonly IColorRepository colorRepository;

        public GetColorQueryHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        public async override Task<GetColorQueryResult> Handle(GetColorQuery request, CancellationToken cancellationToken)
        {
            var result = await colorRepository.GetColors(null);

            return new GetColorQueryResult(result.ToList());
        }
    }
}
