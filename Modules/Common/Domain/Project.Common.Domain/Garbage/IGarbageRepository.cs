namespace Project.Common.Domain.Garbage
{
    public interface IGarbageRepository
    {
        Task<IEnumerable<int>> GetOrderNotYet();
    }
}
