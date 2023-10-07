namespace Project.Product.Domain.Colors
{
    public interface IColorRepository
    {
        Task<IEnumerable<ColorInfo>> GetColors();
    }
}
