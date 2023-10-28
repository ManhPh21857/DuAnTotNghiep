using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Trademarks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkCommand : ICommand<UpdateTrademarkCommandResult>
    {
        public IEnumerable<TrademarkInfo> Trademarks { get; set; }

        public UpdateTrademarkCommand(IEnumerable<TrademarkInfo> trademarks)
        {
            Trademarks = trademarks;
        }
    }
}
