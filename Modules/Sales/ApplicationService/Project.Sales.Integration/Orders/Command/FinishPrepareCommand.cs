using Project.Core.ApplicationService.Commands;

namespace Project.Sales.Integration.Orders.Command
{
    public class FinishPrepareCommand : ICommand<FinishPrepareCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public FinishPrepareCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
