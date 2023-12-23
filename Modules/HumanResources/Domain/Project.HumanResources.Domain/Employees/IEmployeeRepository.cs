namespace Project.HumanResources.Domain.Employees
{
    public interface IEmployeeRepository
    {
        Task<GetEmployeeModel> GetEmployees(int skip, int take);
        Task CreateEmployee(CreateEmployeeParam param);
        Task<EmployeeUser> GetEmployee(int id);
        Task UpdateEmployee(UpdateEmployeeParam param);
        Task DeleteEmployee(DeleteEmployeeParam param);
        Task<int?> GetEmployeeId(int userId);
    }
}
