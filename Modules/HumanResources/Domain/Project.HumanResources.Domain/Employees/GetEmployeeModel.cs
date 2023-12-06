namespace Project.HumanResources.Domain.Employees
{
    public class GetEmployeeModel
    {
        public IEnumerable<EmployeeInfo> Employees { get; set; }
        public int TotalEmployee { get; set; }
    }
}
