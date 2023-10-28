using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Trademarks;
using Project.Product.Integration.Trademarks.Command;

namespace Project.Product.ApplicationService.Trademarks.Command
{
    public class DeleteTrademarkQueryHandler : CommandHandler<DeleteTrademarkCommand, DeleteTrademarkCommandResult>
    {
        private readonly ITrademarkRepository trademark;
        public DeleteTrademarkQueryHandler(ITrademarkRepository trademark)
        {
            this.trademark = trademark;
        }
        public async override Task<DeleteTrademarkCommandResult> Handle(DeleteTrademarkCommand request, CancellationToken cancellationToken)
        {
            var param = new TrademarkInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.trademark.DeleteTrademark(param);

            return new DeleteTrademarkCommandResult(true);
        }
    }
}
