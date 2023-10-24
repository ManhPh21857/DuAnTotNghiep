using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Trademarks;
using Project.Product.Integration.Trademarks.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Trademarks.Command
{
    public class UpdateTrademarkQueryHandler : CommandHandler<UpdateTrademarkCommand, UpdateTrademarkCommandResult>
    {
        private readonly ITrademarkRepository trademark;
        public UpdateTrademarkQueryHandler(ITrademarkRepository trademark)
        {
            this.trademark = trademark;
        }
        public async override Task<UpdateTrademarkCommandResult> Handle(UpdateTrademarkCommand request, CancellationToken cancellationToken)
        {
            var update = new TrademarkInfo()
            {
                Id = request.Id,
                Name = request.Name
            };
            var check = await trademark.CheckTrademarkName(request.Name);
            if (check is not null)
            {
                throw new Exception();
            }
            await trademark.UpdateTrademark(update);
            return new UpdateTrademarkCommandResult(true);
        }
    }
}
