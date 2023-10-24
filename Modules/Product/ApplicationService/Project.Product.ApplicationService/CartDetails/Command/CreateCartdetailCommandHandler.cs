
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.CartDetails;
using Project.Product.Integration.CartDetails.Command;

namespace Project.Product.ApplicationService.CartDetails.Command
{
    public class CreateCartdetailCommandHandler : CommandHandler<CreateCartdetailCommand, CreateCartdetailCommandResult>
    {
        private readonly ICartdetailRepository cardetailRepository;
        public CreateCartdetailCommandHandler(ICartdetailRepository cartdetail)
        {
            this.cardetailRepository = cartdetail;
        }
      

        public async override Task<CreateCartdetailCommandResult> Handle(CreateCartdetailCommand request, CancellationToken cancellationToken)
        {
            var create = new CartdetailInfo()
            {
                Cart_id = request.Cart_id,
                Product_detail_id = request.Product_detail_id,
                Price = request.Price,
                Quantity = request.Quantity
            };
           
            await cardetailRepository.CreateCartdetai(create);
            return new CreateCartdetailCommandResult(true);
        }
    }
}
