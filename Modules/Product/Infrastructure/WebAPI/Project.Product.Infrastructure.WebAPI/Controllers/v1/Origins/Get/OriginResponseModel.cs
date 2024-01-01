namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Get
{
    public class OriginResponseModel
    {
        public IEnumerable<OriginModel> Origins { get; set; }

        public OriginResponseModel()
        {
            Origins = new List<OriginModel>();
        }
    }
}
