using Project.Core.ApplicationService.Queries;

namespace Project.Common.ApplicationService.Tests.Query;

public class TestQuery : IQuery<TestQueryResult> {
    public int Id { get; set; }

    public TestQuery(int id) {
        this.Id = id;
    }
}