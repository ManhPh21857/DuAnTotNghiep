using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles.Query
{
    public class GetGroupRoleQueryResult
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? DataVersion { get; set; }
        public IEnumerable<RoleInfo> Roles { get; set; }

        public GetGroupRoleQueryResult(
            int? id,
            string? name,
            string? description,
            byte[]? dateVersion,
            IEnumerable<RoleInfo> roles
        )
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.DataVersion = dateVersion;
            this.Roles = roles;
        }
    }
}
