using Project.HumanResources.Domain.Employees;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class GetEmployeeQueryResult
    {
        public EmployeeUser Employee { get; set; }

        public GetEmployeeQueryResult(EmployeeUser employee)
        {
            this.Employee = employee;
        }
    }
}
