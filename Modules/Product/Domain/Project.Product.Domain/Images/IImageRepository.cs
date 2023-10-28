namespace Project.Product.Domain.Images
{
    public interface IImageRepository
    {
        Task<int> CreateImage(int productId, string url);
        Task DeleteImage(int productId);
    }
}
