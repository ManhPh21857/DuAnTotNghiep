using Project.Core.ApplicationService.Queries;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class GetEmployeeQuery : IQuery<GetEmployeeQueryResult>
    {
        public int Id { get; set; }

        public GetEmployeeQuery(int id)
        {
            this.Id = id;
        }
    }
}
