using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Command;

namespace Project.Product.ApplicationService.Products.Command
{
    public class DeleteProductCommandHandler : CommandHandler<DeleteProductCommand, DeleteProductCommandResult>
    {
        private readonly IProductRepository productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async override Task<DeleteProductCommandResult> Handle(
            DeleteProductCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            foreach (var item in request.Products)
            {
                await this.productRepository.DeleteProduct(new DeleteProductParam
                {
                    Id = item.Id,
                    DataVersion = item.DataVersion
                });
                await this.productRepository.DeleteProductColor(item.Id);
                await this.productRepository.DeleteProductSize(item.Id);
                await this.productRepository.DeleteProductDetailByProductId(item.Id);
            }

            scope.Complete();

            return new DeleteProductCommandResult(true);
        }
    }
}
