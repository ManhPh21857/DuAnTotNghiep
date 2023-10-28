using Project.Product.Domain.Classifications;

namespace Project.Product.Integration.Classifications.Query
{
    public class GetClassificationCommandResult
    {
        public IList<ClassificationInfo> Classifications { get; set; }

        public GetClassificationCommandResult(IList<ClassificationInfo> classifications)
        {
            Classifications = classifications;
        }
    }
}
