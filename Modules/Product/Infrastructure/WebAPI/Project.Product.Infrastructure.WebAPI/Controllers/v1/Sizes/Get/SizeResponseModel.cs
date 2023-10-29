
namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Get
{
    public class SizeResponseModel
    {
        public IEnumerable<SizeModel> Sizes { get; set; }

        public SizeResponseModel()
        {
            Sizes = new List<SizeModel>();
        }
    }
}
