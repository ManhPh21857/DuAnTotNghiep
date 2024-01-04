namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Roles.Get
{
    public class GetGroupListResponseModel
    {
        public IEnumerable<GroupModel> Groups { get; set; }
        public int TotalPage { get; set; }

        public GetGroupListResponseModel()
        {
            this.Groups = new List<GroupModel>();
        }
    }
}
