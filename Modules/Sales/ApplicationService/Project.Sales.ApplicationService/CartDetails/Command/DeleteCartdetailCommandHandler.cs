
using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.CartDetails;
using Project.Sales.Integration.CartDetails.Command;

namespace Project.Sales.ApplicationService.CartDetails.Command
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
            var param = new CartDetailInfo { CartId = request.CartId, ProductDetailId = request.ProductDetailId, DataVersion = request.DataVersion };

            await this.cardetailRepository.DeleteCartdetai(param);

            return new DeleteCartdetailCommandResult(true);

        }
    }
}
