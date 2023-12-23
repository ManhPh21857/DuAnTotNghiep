namespace Project.HumanResources.Integration.Users.Command
{
    public class ChangePasswordCommandResult
    {
        public bool IsSuccess { get; set; }

        public ChangePasswordCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
