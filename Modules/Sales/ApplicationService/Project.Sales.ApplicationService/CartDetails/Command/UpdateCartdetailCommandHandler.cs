using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Command;

namespace Project.Sales.ApplicationService.CartDetails.Command
{
    public class UpdateCartdetailCommandHandler : CommandHandler<UpdateCartdetailCommand, UpdateCartdetailCommandResult>
    {
        private readonly ICartdetailRepository cartdetailRepository;

        public UpdateCartdetailCommandHandler(ICartdetailRepository cartdetail)
        {
            this.cartdetailRepository = cartdetail;
        }
        public async override Task<UpdateCartdetailCommandResult> Handle(UpdateCartdetailCommand request, CancellationToken cancellationToken)
        {
            CartDetailInfo update = new CartDetailInfo
            {
                ProductDetailId = request.ProductDetailId,
                Quantity = request.Quantity,
                DataVersion = request.DataVersion
            };
            await this.cartdetailRepository.UpdateCartdetai(update);
            return new UpdateCartdetailCommandResult(true);
        }
    }
}
