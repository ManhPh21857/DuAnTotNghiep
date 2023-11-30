using Project.HumanResources.Domain.Employees;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class EmployeeQueryResult
    {
        public IEnumerable<EmployeeInfo> Employees { get; set; }

        public EmployeeQueryResult(IEnumerable<EmployeeInfo> employees)
        {
            this.Employees = employees;
        }
    }
}
