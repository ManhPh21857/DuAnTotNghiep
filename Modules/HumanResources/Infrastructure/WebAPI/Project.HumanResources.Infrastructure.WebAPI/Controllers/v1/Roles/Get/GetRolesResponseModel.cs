namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get
{
    public class GetRolesResponseModel
    {
        public IEnumerable<RoleModel> Roles { get; set; }

        public GetRolesResponseModel()
        {
            this.Roles = new List<RoleModel>();
        }
    }
}
