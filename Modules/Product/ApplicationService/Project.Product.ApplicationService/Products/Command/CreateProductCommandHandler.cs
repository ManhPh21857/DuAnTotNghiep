using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Images;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Command;

namespace Project.Product.ApplicationService.Products.Command
{
    public class CreateProductCommandHandler : CommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IImageRepository imageRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, IImageRepository imageRepository)
        {
            this.productRepository = productRepository;
            this.imageRepository = imageRepository;
        }
        public async override Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            using var scope = TransactionFactory.Create();

            //insert product
            var createProductParam = new ProductInfo
            {
                Code = request.Code,
                Name = request.Name,
                ClassificationId = request.ClassificationId,
                MaterialId = request.MaterialId,
                SupplierId = request.SupplierId,
                TrademarkId = request.TrademarkId,
                OriginId = request.OriginId,
                Description = request.Description
            };

            int productId = await this.productRepository.CreateProduct(createProductParam);
            
            //insert image/ product image

            foreach (string image in request.ProductImages)
            {
                await this.imageRepository.CreateImage(productId, image);
            }

            //insert product color

            var colorDictionary = new Dictionary<int, int>();

            foreach (var productColor in request.ProductColors)
            {
                int imageId = await this.imageRepository.CreateImage(productId, productColor.Image);

                int productColorId = await this.productRepository.CreateProductColor(productId, productColor.ColorId, imageId);

                colorDictionary.TryAdd(productColor.ColorId, productColorId);
            }

            //insert product size

            foreach (var productSize in request.ProductDetails.DistinctBy(x=>x.SizeId))
            {
                await this.productRepository.CreateProductSize(productId, productSize.SizeId);
            }

            //insert product detail
            foreach (var productDetail in request.ProductDetails)
            {
                if (!colorDictionary.TryGetValue(productDetail.ColorId, out int colorId))
                {
                    throw new ArgumentNullException();
                }

                productDetail.ProductId = productId;
                productDetail.ColorId = colorId;
                await this.productRepository.CreateProductDetail(productDetail);
            }

            scope.Complete();

            return new CreateProductCommandResult(true);
        }
    }
}
