using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Authentication.Forgot
{
    public class ForgotRequest : ICommand<ForgotResponse>
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
