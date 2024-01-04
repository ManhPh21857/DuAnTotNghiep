namespace Project.HumanResources.Integration.Roles.Command
{
    public class DeleteGroupCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteGroupCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
