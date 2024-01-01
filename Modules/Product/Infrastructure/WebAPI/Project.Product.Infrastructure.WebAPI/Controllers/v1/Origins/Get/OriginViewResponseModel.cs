namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Origins.Get
{
    public class OriginViewResponseModel
    {
        public IEnumerable<OriginViewModel> Origins { get; set; }

        public OriginViewResponseModel()
        {
            this.Origins = new List<OriginViewModel>();
        }
    }
}
