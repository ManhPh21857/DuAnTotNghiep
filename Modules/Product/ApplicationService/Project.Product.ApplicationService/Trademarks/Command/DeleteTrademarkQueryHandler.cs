using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Trademarks;
using Project.Product.Integration.Trademarks.Command;

namespace Project.Product.ApplicationService.Trademarks.Command
{
    public class DeleteTrademarkQueryHandler : CommandHandler<DeleteTrademarkQuery, DeleteTrademarkQueryResult>
    {
        private readonly ITrademarkRepository trademark;
        public DeleteTrademarkQueryHandler(ITrademarkRepository trademark)
        {
            this.trademark = trademark;
        }
        public async override Task<DeleteTrademarkQueryResult> Handle(DeleteTrademarkQuery request, CancellationToken cancellationToken)
        {
            var delete = new TrademarkInfo()
            {
                Id = request.Id
            };
            await trademark.DeleteTrademark(delete);
            return new DeleteTrademarkQueryResult(true);
        }
    }
}
