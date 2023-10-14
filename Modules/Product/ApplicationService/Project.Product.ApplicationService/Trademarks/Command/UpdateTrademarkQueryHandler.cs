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
    public class UpdateTrademarkQueryHandler : CommandHandler<UpdateTrademarkQuery, UpdateTrademarkQueryResult>
    {
        private readonly ITrademarkRepository trademark;
        public UpdateTrademarkQueryHandler(ITrademarkRepository trademark)
        {
            this.trademark = trademark;
        }
        public async override Task<UpdateTrademarkQueryResult> Handle(UpdateTrademarkQuery request, CancellationToken cancellationToken)
        {
            var update = new TrademarkInfo()
            {
                Id = request.Id,
                Name = request.Name
            };
            await trademark.UpdateTrademark(update);
            return new UpdateTrademarkQueryResult(true);
        }
    }
}
