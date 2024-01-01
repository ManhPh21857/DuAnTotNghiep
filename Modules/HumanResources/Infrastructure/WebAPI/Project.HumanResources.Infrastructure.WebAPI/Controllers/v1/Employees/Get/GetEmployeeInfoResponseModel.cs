using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Get
{
    public class GetEmployeeInfoResponseModel
    {
        public EmployeeInfoModel? Employee { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }

        public GetEmployeeInfoResponseModel()
        {
            this.Roles = new List<RoleModel>();
        }
    }
}
