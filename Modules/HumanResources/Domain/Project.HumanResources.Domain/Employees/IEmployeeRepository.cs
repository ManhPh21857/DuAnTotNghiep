namespace Project.HumanResources.Domain.Employees
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeInfo>> GetEmployees(int skip, int take);

        Task CreateEmployee(CreateEmployeeParam param);
    }
}
