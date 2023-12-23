using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Orders.Command
{
    public class AssignOrderCommand : ICommand<AssignOrderCommandResult>
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public byte[]? DataVersion { get; set; }

        public AssignOrderCommand(int id, int employeeId, byte[]? dataVersion)
        {
            this.Id = id;
            this.EmployeeId = employeeId;
            this.DataVersion = dataVersion;
        }
    }
}
