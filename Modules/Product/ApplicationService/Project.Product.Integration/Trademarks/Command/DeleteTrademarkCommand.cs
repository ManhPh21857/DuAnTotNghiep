using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Trademarks.Command
{
    public class DeleteTrademarkCommand : ICommand<DeleteTrademarkCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public DeleteTrademarkCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
