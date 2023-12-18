using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Customers.Command
{
    public class UpdateCustomerCommand : ICommand<UpdateCustomerCommandResult>
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string Image { get; set; }
        public int Sex { get; set; }
        public byte[]? DataVersion { get; set; }

        public UpdateCustomerCommand(
            int id,
            string lastName,
            string firstName,
            string phoneNumber,
            DateTime birthday,
            string image,
            int sex,
            byte[]? dataVersion
        )
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.PhoneNumber = phoneNumber;
            this.Birthday = birthday;
            this.Image = image;
            this.Sex = sex;
            this.DataVersion = dataVersion;
        }
    }
}
