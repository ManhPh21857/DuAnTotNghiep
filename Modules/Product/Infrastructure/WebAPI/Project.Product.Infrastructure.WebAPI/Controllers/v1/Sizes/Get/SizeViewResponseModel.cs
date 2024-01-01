namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Get
{
    public class SizeViewResponseModel
    {
        public IEnumerable<SizeViewModel> Sizes { get; set; }

        public SizeViewResponseModel()
        {
            this.Sizes = new List<SizeViewModel>();
        }
    }
}
