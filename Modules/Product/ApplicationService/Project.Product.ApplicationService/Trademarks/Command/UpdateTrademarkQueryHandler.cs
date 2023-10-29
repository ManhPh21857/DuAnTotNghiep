using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Trademarks;
using Project.Product.Integration.Trademarks.Command;



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
            foreach (var item in request.Trademarks)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    var check = await trademark.CheckTrademarkName(item.Name);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.trademark.CreateTrademark(item);
                }
                else
                {
                    var check = await trademark.CheckTrademarkName(item.Name);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.trademark.UpdateTrademark(item);
                }
            }

            return new UpdateTrademarkCommandResult(true);
        }
    }
}
