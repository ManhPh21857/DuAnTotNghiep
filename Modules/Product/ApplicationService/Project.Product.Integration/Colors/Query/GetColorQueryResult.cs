using Project.Product.Domain.Colors;

namespace Project.Product.Integration.Colors.Query
{
    public class GetColorQueryResult
    {
        public IEnumerable<ColorInfo> Colors { get; set; }

        public GetColorQueryResult(IEnumerable<ColorInfo> colors)
        {
            this.Colors = colors;
        }
    }
}
