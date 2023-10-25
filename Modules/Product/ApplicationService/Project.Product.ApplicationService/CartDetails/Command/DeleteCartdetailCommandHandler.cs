
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.CartDetails;
using Project.Product.Integration.CartDetails.Command;

namespace Project.Product.ApplicationService.CartDetails.Command
{
    public class DeleteCartdetailCommandHandler : CommandHandler<DeleteCartdetailCommand, DeleteCartdetailCommandResult>
    {
        private readonly ICartdetailRepository cardetailRepository;
        public DeleteCartdetailCommandHandler(ICartdetailRepository cartdetail)
        {
            this.cardetailRepository = cartdetail;
        }


        public async override Task<DeleteCartdetailCommandResult> Handle(DeleteCartdetailCommand request, CancellationToken cancellationToken)
        {
            var delete = new CartdetailInfo()
            {
                Cartid = request.Cartid,
                Productdetailid = request.Productdetailid
            };
            await cardetailRepository.DeleteCartdetai(delete);
            return new DeleteCartdetailCommandResult(true);
        }
    }
}
