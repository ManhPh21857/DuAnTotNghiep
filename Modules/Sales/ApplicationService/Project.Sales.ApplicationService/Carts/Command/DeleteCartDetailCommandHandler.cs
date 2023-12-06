using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Sales.Domain.Carts;
using Project.Sales.Integration.Carts.Command;

namespace Project.Sales.ApplicationService.Carts.Command
{
    public class DeleteCartDetailCommandHandler : CommandHandler<DeleteCartDetailCommand, DeleteCartDetailCommandResult>
    {
        private readonly ICartRepository cartRepository;
        private readonly ISessionInfo sessionInfo;

        public DeleteCartDetailCommandHandler(ICartRepository cartRepository, ISessionInfo sessionInfo)
        {
            this.cartRepository = cartRepository;
            this.sessionInfo = sessionInfo;
        }
        public async override Task<DeleteCartDetailCommandResult> Handle(
            DeleteCartDetailCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            int userId = this.sessionInfo.UserId.value;
            int cartId = await this.cartRepository.FindCartId(userId);

            foreach (var item in request.CartDetails)
            {
                item.CartId = cartId;
                await this.cartRepository.DeleteCartDetail(item);
            }

            scope.Complete();
            return new DeleteCartDetailCommandResult(true);
        }
    }
}
