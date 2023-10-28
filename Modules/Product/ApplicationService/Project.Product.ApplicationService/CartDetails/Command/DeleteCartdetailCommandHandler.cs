
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
            var param = new CartdetailInfo { CartId = request.CartId, ProductDetailId = request.ProductDetailId, DataVersion = request.DataVersion };

            await this.cardetailRepository.DeleteCartdetai(param);

            return new DeleteCartdetailCommandResult(true);
        }
    }
}
