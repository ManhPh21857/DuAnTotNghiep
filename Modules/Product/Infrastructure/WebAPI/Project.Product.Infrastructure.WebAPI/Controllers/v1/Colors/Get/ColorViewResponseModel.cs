namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Get
{
    public class ColorViewResponseModel
    {
        public IEnumerable<ColorViewModel> Colors { get; set; }

        public ColorViewResponseModel()
        {
            this.Colors = new List<ColorViewModel>();
        }
    }
}
