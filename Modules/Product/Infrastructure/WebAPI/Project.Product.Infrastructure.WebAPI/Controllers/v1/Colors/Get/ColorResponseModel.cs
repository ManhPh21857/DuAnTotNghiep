namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Get
{
    public class ColorResponseModel
    {
        public IEnumerable<ColorModel> Colors { get; set; }

        public ColorResponseModel()
        {
            Colors = new List<ColorModel>();
        }
    }
}
