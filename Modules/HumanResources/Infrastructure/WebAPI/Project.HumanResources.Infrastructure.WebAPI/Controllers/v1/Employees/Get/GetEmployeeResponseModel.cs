namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Get
{
    public class GetEmployeeResponseModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }

        public GetEmployeeResponseModel(IEnumerable<EmployeeModel> employees)
        {
            this.Employees = employees;
        }
    }
}
