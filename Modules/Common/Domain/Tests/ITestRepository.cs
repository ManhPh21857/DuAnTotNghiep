namespace Project.Common.Domain.Tests; 

public interface ITestRepository {
    Task<TestInfo> GetTestInfo(int id);
}