using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Images;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Command;

namespace Project.Product.ApplicationService.Products.Command
{
    public class DeleteProductCommandHandler : CommandHandler<DeleteProductCommand, DeleteProductCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IImageRepository imageRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository, IImageRepository imageRepository)
        {
            this.productRepository = productRepository;
            this.imageRepository = imageRepository;
        }
        public async override Task<DeleteProductCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            await this.productRepository.DeleteProduct(id);
            await this.productRepository.DeleteProductColor(id);
            await this.productRepository.DeleteProductSize(id);
            await this.productRepository.DeleteProductDetailByProductId(id);

            await this.imageRepository.DeleteImage(id);

            return new DeleteProductCommandResult(true);
        }
    }
}
