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
    public class CreateTrademarkQueryHandler : CommandHandler<CreateTrademarkQuery, CreateTrademarkQueryResult>
    {
        private readonly ITrademarkRepository trademark;
        public CreateTrademarkQueryHandler(ITrademarkRepository trademark)
        {
            this.trademark = trademark;
        }
  
        public async override Task<CreateTrademarkQueryResult> Handle(CreateTrademarkQuery request, CancellationToken cancellationToken)
        {
            var create = new TrademarkInfo()
            {
                Name = request.Name,
            };
            await trademark.CreateTrademark(create);
            return new CreateTrademarkQueryResult(true);
        }
    }
}
