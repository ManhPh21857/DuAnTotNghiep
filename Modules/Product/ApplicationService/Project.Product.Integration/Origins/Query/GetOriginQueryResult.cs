using Project.Product.Domain.Origins;

namespace Project.Product.Integration.Origins.Query
{
    public class GetOriginQueryResult
    {
        public IEnumerable<OriginInfo> Origins { get; set; }
        public GetOriginQueryResult(IEnumerable<OriginInfo> origins)
        {
            this.Origins = origins;
        }
    }
}
