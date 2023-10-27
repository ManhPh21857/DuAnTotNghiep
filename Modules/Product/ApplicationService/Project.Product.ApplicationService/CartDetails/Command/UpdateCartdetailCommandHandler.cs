

using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.CartDetails;
using Project.Product.Integration.CartDetails.Command;

namespace Project.Product.ApplicationService.CartDetails.Command
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
                if (item.DataVersion.IsNullOrEmpty())
                {
                    await this.cartdetailRepository.CreateCartdetai(item);
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
