namespace Project.Product.Domain.Classifications
{
    public interface IClassificationRepository
    {
        Task<IEnumerable<ClassificationInfo>> GetClassification(int? id);
        Task CreateClassification(ClassificationInfo classification);
        Task UpdateClassification(ClassificationInfo classification);
        Task DeleteClassification(ClassificationInfo classification);
        Task ReActiveClassification(ClassificationInfo classification);
        Task<ClassificationInfo> CheckClassificationName(string name);
    }
}
