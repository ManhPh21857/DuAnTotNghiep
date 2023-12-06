using Project.HumanResources.Domain.Employees;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class EmployeeQueryResult
    {
        public IEnumerable<EmployeeInfo> Employees { get; set; }
        public int TotalPage { get; set; }

        public EmployeeQueryResult(IEnumerable<EmployeeInfo> employees, int totalPage)
        {
            this.Employees = employees;
            this.TotalPage = totalPage;
        }
    }
}
