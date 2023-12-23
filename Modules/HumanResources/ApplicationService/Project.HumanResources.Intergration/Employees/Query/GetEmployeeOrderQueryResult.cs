using Project.HumanResources.Domain.Employees;

namespace Project.HumanResources.Integration.Employees.Query
{
    public class GetEmployeeOrderQueryResult
    {
        public IEnumerable<EmployeeOrder> EmployeeOrders { get; set; }

        public GetEmployeeOrderQueryResult(IEnumerable<EmployeeOrder> employeeOrders)
        {
            this.EmployeeOrders = employeeOrders;
        }
    }
}
