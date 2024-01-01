using Project.Product.Domain.Classifications;

namespace Project.Product.Integration.Classifications.Query
{
    public class GetClassificationQueryResult
    {
        public IEnumerable<ClassificationInfo> Classifications { get; set; }

        public GetClassificationQueryResult(IEnumerable<ClassificationInfo> classifications)
        {
            this.Classifications = classifications;
        }
    }
}
