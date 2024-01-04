using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Roles.Command
{
    public class UpdateGroupRoleQuery : ICommand<UpdateGroupRoleQueryResult>
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? DataVersion { get; set; }
        public List<int> Roles { get; set; }

        public UpdateGroupRoleQuery(
            int? id,
            string name,
            string description,
            byte[]? dataVersion,
            List<int> roles
        )
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.DataVersion = dataVersion;
            this.Roles = roles;
        }
    }
}
