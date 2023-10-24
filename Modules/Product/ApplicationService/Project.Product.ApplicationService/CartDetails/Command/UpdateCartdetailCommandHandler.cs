

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
            var update = new CartdetailInfo()
            {
                Cart_id = request.Cart_id,
                Product_detail_id = request.Product_detail_id,
                Price = request.Price,
                Quantity = request.Quantity
            };

            await cartdetailRepository.UpdateCartdetai(update);


            return new UpdateCartdetailCommandResult(true);
        }
    }
}
