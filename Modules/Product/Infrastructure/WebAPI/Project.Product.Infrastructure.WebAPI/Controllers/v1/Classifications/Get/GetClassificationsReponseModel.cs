using Project.Product.Domain.Classifications;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Get
{
    public class GetClassificationsReponseModel
    {
        public IEnumerable<ClassificationInfo> Classifications { get; set; }
        public GetClassificationsReponseModel()
        {
            Classifications = new List<ClassificationInfo>();
        }
    }
}
