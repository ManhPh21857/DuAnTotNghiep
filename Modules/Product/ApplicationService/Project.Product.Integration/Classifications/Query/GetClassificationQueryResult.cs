using Project.Product.Domain.Classifications;

namespace Project.Product.Integration.Classifications.Query
{
    public class GetClassificationQueryResult
    {
        public IList<ClassificationInfo> Classifications { get; set; }

        public GetClassificationQueryResult(IList<ClassificationInfo> classifications)
        {
            Classifications = classifications;
        }
    }
}
