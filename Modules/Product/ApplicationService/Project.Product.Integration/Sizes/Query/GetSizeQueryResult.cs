using Project.Product.Domain.Sizes;


namespace Project.Product.Integration.Sizes.Query
{
    public class GetSizeQueryResult
    {
        public IList<SizeInfo> Sizes { get; set; }

        public GetSizeQueryResult(IList<SizeInfo> sizes)
        {
            Sizes = sizes;
        }
    }
}
