

using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Trademarks;
using Project.Product.Integration.Trademarks.Command;

namespace Project.Product.ApplicationService.Trademarks.Command
{
    public class ReactiveTrademarkCommandHandler : CommandHandler<ReactiveTrademarkCommand, ReactiveTrademarkCommandResult>
    {
        private readonly ITrademarkRepository trademarkRepository;

        public ReactiveTrademarkCommandHandler(ITrademarkRepository trademarkRepository)
        {
            this.trademarkRepository = trademarkRepository;
        }
       
        public async override Task<ReactiveTrademarkCommandResult> Handle(ReactiveTrademarkCommand request, CancellationToken cancellationToken)
        {
            var param = new TrademarkInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.trademarkRepository.ReactiveTrademark(param);

            return new ReactiveTrademarkCommandResult(true);
        }
    }
}
