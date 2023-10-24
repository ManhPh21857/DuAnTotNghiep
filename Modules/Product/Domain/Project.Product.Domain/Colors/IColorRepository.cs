namespace Project.Product.Domain.Colors
{
    public interface IColorRepository
    {
        Task<IEnumerable<ColorInfo>> GetColors(int? id);
        Task CreateColor(ColorInfo param);
        Task UpdateColor(ColorInfo param);
        Task DeleteColor(ColorInfo param);
        Task ReActiveColor(ColorInfo param);
    }
}
