using Project.Core.ApplicationService.Queries;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class EmployeeQuery : IQuery<EmployeeQueryResult>
    {
        public int PageNumber { get; set; }

        public EmployeeQuery(int pageNumber)
        {
            this.PageNumber = pageNumber;
        }
    }
}
