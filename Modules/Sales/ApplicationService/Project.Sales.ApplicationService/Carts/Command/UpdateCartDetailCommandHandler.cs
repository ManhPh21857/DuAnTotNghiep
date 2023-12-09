using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Product.Domain.Products;
using Project.Sales.Domain.Carts;
using Project.Sales.Integration.Carts.Command;

namespace Project.Sales.ApplicationService.Carts.Command
{
    public class UpdateCartDetailCommandHandler : CommandHandler<UpdateCartDetailCommand, UpdateCartDetailCommandResult>
    {
        private readonly ICartRepository cartRepository;
        private readonly IProductRepository productRepository;
        private readonly ISessionInfo sessionInfo;

        public UpdateCartDetailCommandHandler(
            ICartRepository cartRepository,
            IProductRepository productRepository,
            ISessionInfo sessionInfo
        )
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateCartDetailCommandResult> Handle(
            UpdateCartDetailCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            //get product detail
            var productDetail = await this.productRepository.GetProductDetailById(request.ProductDetailId);

            //enough => delete old, add new
            if (productDetail is null)
            {
                var ex = new DomainException("", "sản phẩm không tồn tại");

                throw ex;
            }

            if (productDetail.Quantity < request.Quantity)
            {
                var ex = new DomainException("", "số lượng không đủ");

                throw ex;
            }

            int userId = this.sessionInfo.UserId.value;
            int? cartId = await this.cartRepository.FindCartId(userId);
            if (cartId is null)
            {
                var ex = new DomainException("", "somethings went wrong!");

                throw ex;
            }

            await this.cartRepository.DeleteCartDetail(new DeleteCartDetailParam
                {
                    CartId = cartId.Value,
                    ProductDetailId = request.ProductDetailId,
                    DataVersion = request.DataVersion
                }
            );

            await this.cartRepository.CreateCartDetail(new CreateCartDetailParam
                {
                    CartId = cartId.Value,
                    ProductDetailId = request.ProductDetailIdNew,
                    Quantity = request.Quantity
                }
            );

            scope.Complete();
            return new UpdateCartDetailCommandResult(true);
        }
    }
}
