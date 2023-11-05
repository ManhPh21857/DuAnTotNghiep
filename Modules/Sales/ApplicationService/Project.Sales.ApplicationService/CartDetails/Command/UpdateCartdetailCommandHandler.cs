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
            foreach (var item in request.Cartdetails)
            {
                var check = await this.cartdetailRepository.CheckCartdetailName((int)item.CartId,(int)item.ProductDetailId);
                if (item.DataVersion.IsNullOrEmpty())
                {
                    if (check != null)
                    {
                        await this.cartdetailRepository.UpdateQuantityCartdetail((int)item.CartId,(int)item.ProductDetailId);
                    }
                    else
                    {
                        await this.cartdetailRepository.CreateCartdetai(item);
                    }
                    //await this.cartdetailRepository.CreateCartdetai(item);
                }
                else
                {
                    await this.cartdetailRepository.UpdateCartdetai(item);
                    
                }
            }

            return new UpdateCartdetailCommandResult(true);
        }
    }
}
