using Project.Product.Domain.Sizes;

namespace Project.Product.Integration.Sizes.Query
{
    public class GetSizeQueryResult
    {
        public IEnumerable<SizeInfo> Sizes { get; set; }

        public GetSizeQueryResult(IEnumerable<SizeInfo> sizes)
        {
            this.Sizes = sizes;
        }
    }
}
