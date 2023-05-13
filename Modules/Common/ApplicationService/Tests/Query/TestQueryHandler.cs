using Project.Common.Domain.Tests;
using Project.Core.ApplicationService.Queries;

namespace Project.Common.ApplicationService.Tests.Query; 

public class TestQueryHandler : QueryHandler<TestQuery, TestQueryResult> {
    private readonly ITestRepository testRepository;

    public TestQueryHandler(ITestRepository testRepository)
    {
        this.testRepository = testRepository;
    }

    public override async Task<TestQueryResult> Handle(TestQuery request, CancellationToken cancellationToken) {
        var result = await testRepository.GetTestInfo(request.Id);
        string name = result.UserName;
        return new TestQueryResult(name);
    }
}