
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Trademarks.Command
{
    public class ReactiveTrademarkCommand : ICommand<ReactiveTrademarkCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public ReactiveTrademarkCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
