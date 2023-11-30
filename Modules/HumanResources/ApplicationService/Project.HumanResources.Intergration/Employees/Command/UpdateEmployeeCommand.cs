using Project.Core.ApplicationService.Commands;
using Project.Core.Domain.Enums;

namespace Project.HumanResources.Integration.Employees.Command
{
    public class UpdateEmployeeCommand : ICommand<UpdateEmployeeCommandResult>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Sex { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        public UpdateEmployeeCommand(
            string email,
            string username,
            string password,
            int id,
            string firstName,
            string lastName,
            string image,
            string address,
            DateTime birthday,
            int sex,
            string phoneNumber,
            IEnumerable<Role> roles
        )
        {
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Image = image;
            this.Address = address;
            this.Birthday = birthday;
            this.Sex = sex;
            this.PhoneNumber = phoneNumber;
            this.Roles = roles;
        }
    }
}
