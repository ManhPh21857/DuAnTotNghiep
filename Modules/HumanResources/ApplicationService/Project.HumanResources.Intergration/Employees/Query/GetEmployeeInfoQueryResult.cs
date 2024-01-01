using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class GetEmployeeInfoQueryResult
    {
        public EmployeeInfo Employee { get; set; }
        public IEnumerable<RoleInfo> Roles { get; set; }

        public GetEmployeeInfoQueryResult(EmployeeInfo employee, IEnumerable<RoleInfo> roles)
        {
            this.Employee = employee;
            this.Roles = roles;
        }
    }
}
