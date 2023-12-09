namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get
{
    public class GetGroupsResponseModel
    {
        public IEnumerable<GroupModel> Groups { get; set; }
        public IEnumerable<GroupRoleModel> GroupRoles { get; set; }

        public GetGroupsResponseModel()
        {
            this.Groups = new List<GroupModel>();
            this.GroupRoles = new List<GroupRoleModel>();

        }
    }
}
