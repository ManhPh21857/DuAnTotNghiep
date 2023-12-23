using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Users.Command
{
    public class ChangePasswordCommand : ICommand<ChangePasswordCommandResult>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            this.OldPassword = oldPassword;
            this.NewPassword = newPassword;
        }
    }
}
