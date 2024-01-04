using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get
{
    public class GetGroupRoleResponseModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? DataVersion { get; set; }
        public IEnumerable<RoleInfo> Roles { get; set; }

        public GetGroupRoleResponseModel()
        {
            this.Roles = new List<RoleInfo>();
        }
    }
}
