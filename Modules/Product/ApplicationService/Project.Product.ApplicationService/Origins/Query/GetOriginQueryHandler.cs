using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Origins;
using Project.Product.Integration.Origins.Query;

namespace Project.Product.ApplicationService.Origins.Query
{
    public class GetOriginQueryHandler : QueryHandler<GetOriginQuery, GetOriginQueryResult>
    {
        private readonly IOriginRepository Origin;
        public GetOriginQueryHandler(IOriginRepository origin)
        {
            this.Origin = origin;
        }

        public async override Task<GetOriginQueryResult> Handle(GetOriginQuery request, CancellationToken cancellationToken)
        {
            var result = await Origin.GetOrigin(null);
            return new GetOriginQueryResult(result.ToList());
        }
    }
}
