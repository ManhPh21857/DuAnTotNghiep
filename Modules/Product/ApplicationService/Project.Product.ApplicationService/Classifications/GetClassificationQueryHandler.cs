using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Classifications;
using Project.Product.Integration.Classifications;

namespace Project.Product.ApplicationService.Classifications
{
    public class GetClassificationQueryHandler : QueryHandler<GetClassificationQuery, GetClassificationQueryResult>
    {
        private readonly IClassificationRepository classificationRepository;

        public GetClassificationQueryHandler(IClassificationRepository classificationRepository)
        {
            this.classificationRepository = classificationRepository;
        }

        public async override Task<GetClassificationQueryResult> Handle(GetClassificationQuery request, CancellationToken cancellationToken)
        {
            var result = await classificationRepository.GetClassifications();

            return new GetClassificationQueryResult(result.ToList());
        }
    }
}
