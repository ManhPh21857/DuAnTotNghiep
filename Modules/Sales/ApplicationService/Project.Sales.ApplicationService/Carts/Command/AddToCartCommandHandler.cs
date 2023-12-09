using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Sales.Domain.Carts;
using Project.Sales.Integration.Carts.Command;

namespace Project.Sales.ApplicationService.Carts.Command
{
    public class AddToCartCommandHandler : CommandHandler<AddToCartCommand, AddToCartCommandResult>
    {
        private readonly ICartRepository cartRepository;
        private readonly ISessionInfo sessionInfo;

        public AddToCartCommandHandler(ICartRepository cartRepository, ISessionInfo sessionInfo)
        {
            this.cartRepository = cartRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<AddToCartCommandResult> Handle(
            AddToCartCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            int userId = this.sessionInfo.UserId.value;
            int cartId = await this.cartRepository.GetCartId(userId);

            var cartDetail = await this.cartRepository.FindCartDetail(cartId, request.ProductDetailId);

            if (cartDetail is null)
            {
                await this.cartRepository.CreateCartDetail(new CreateCartDetailParam
                    {
                        CartId = cartId,
                        ProductDetailId = request.ProductDetailId,
                        Quantity = request.Quantity
                    }
                );
            }
            else
            {
                await this.cartRepository.UpdateCartDetail(new CreateCartDetailParam
                    {
                        CartId = cartId,
                        ProductDetailId = request.ProductDetailId,
                        Quantity = cartDetail.Quantity + request.Quantity
                    }
                );
            }

            scope.Complete();
            return new AddToCartCommandResult(true);
        }
    }
}
