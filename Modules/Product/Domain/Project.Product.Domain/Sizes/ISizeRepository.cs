namespace Project.Product.Domain.Sizes
{
    public interface ISizeRepository
    {
        Task<IEnumerable<SizeInfo>> GetSizes(int? id);
        Task CreateSize(SizeInfo size);
        Task UpdateSize(SizeInfo size);
        Task DeleteSize(SizeInfo size);
        Task ReActiveSize(SizeInfo size);
        Task<SizeInfo> GetSizeByName(string name);
    }
}
