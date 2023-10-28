
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.CartDetails;
using Project.Product.Integration.CartDetails.Command;

namespace Project.Product.ApplicationService.CartDetails.Command
{
    public class ReactiveCartdetailCommandHandler : CommandHandler<ReactiveCartdetailCommand, ReactiveCartdetailCommandResult>
    {
        private readonly ICartdetailRepository cartdetailRepository;

        public ReactiveCartdetailCommandHandler(ICartdetailRepository cartdetailRepository)
        {
            this.cartdetailRepository = cartdetailRepository;
        }

        public async override Task<ReactiveCartdetailCommandResult> Handle(ReactiveCartdetailCommand request, CancellationToken cancellationToken)
        {
            var param = new CartdetailInfo { CartId = request.CartId, ProductDetailId = request.ProductDetailId, DataVersion = request.DataVersion };

            await this.cartdetailRepository.ReactiveCartdetail(param);

            return new ReactiveCartdetailCommandResult(true);
        }
    }
}
