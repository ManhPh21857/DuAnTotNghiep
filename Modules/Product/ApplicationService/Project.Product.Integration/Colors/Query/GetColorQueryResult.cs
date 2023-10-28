using Project.Product.Domain.Colors;

namespace Project.Product.Integration.Colors.Query
{
    public class GetColorQueryResult
    {
        public IList<ColorInfo> Colors { get; set; }

        public GetColorQueryResult(IList<ColorInfo> colors)
        {
            Colors = colors;
        }
    }
}
