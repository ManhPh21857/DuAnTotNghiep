using Project.Product.Domain.Origins;

namespace Project.Product.Integration.Origins.Query
{
    public class GetOriginQueryResult
    {
        public IList<OriginInfo> Origins { get; set; }
        public GetOriginQueryResult(IList<OriginInfo> origins)
        {
            Origins = origins;
        }
    }
}
