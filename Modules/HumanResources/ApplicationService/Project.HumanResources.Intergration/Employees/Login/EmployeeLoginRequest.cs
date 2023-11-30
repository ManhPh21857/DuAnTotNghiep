using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Employees.Login
{
    public class EmployeeLoginRequest : ICommand<EmployeeLoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public EmployeeLoginRequest(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
