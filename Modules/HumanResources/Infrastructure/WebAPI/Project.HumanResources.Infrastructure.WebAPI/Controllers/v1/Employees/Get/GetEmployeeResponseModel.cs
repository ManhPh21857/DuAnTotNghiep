namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Get
{
    public class GetEmployeeResponseModel
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public int TotalPage { get; set; }

        public GetEmployeeResponseModel()
        {
            this.Employees = new List<EmployeeModel>();
        }
    }
}
