using Project.Core.ApplicationService.Commands;

namespace Project.HumanResources.Integration.Customers.Command
{
    public class UpdateAddressDefaultCommand : ICommand<UpdateAddressDefaultCommandResult>
    {
        public int Id { get; set; }

        public UpdateAddressDefaultCommand(int id)
        {
            this.Id = id;
        }
    }
}
