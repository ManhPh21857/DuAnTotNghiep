namespace Project.HumanResources.Integration.Roles.Command
{
    public class UpdateGroupRoleQueryResult
    {
        public bool IsSuccess { get; set; }

        public UpdateGroupRoleQueryResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
