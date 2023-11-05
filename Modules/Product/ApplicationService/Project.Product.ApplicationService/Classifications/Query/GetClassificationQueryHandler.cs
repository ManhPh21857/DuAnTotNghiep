using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications.Query;

namespace Project.Product.ApplicationService.Classifications.Query
{
    public class GetClassificationQueryHandler : QueryHandler<GetClassificationQuery, GetClassificationQueryResult>
    {
        private readonly IClassificationRepository classifications;
        public GetClassificationQueryHandler(IClassificationRepository classifications)
        {
            this.classifications = classifications;
        }
        public override async Task<GetClassificationQueryResult> Handle(GetClassificationQuery request, CancellationToken cancellationToken)
        {
            var result = await classifications.GetClassification(null);
            return new GetClassificationQueryResult(result.ToList());
        }
    }
}
