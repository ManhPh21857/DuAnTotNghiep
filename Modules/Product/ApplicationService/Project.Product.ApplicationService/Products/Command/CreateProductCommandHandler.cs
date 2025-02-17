﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.Product.Domain.Products;
using Project.Product.Integration.Products.Command;
using SixLabors.ImageSharp;

namespace Project.Product.ApplicationService.Products.Command
{
    public class CreateProductCommandHandler : CommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IConfiguration configuration;

        public CreateProductCommandHandler(IProductRepository productRepository, IConfiguration configuration)
        {
            this.productRepository = productRepository;
            this.configuration = configuration;
        }

        public async override Task<CreateProductCommandResult> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            using var scope = TransactionFactory.Create();

            int idReturn;

            var imageDictionary = new Dictionary<string, string>();

            var product = request.Product ?? throw new ArgumentNullException();
            if (product is null)
            {
                throw new ArgumentNullException();
            }

            if (this.IsBase64String(product.Image))
            {
                string imageProductName = $"{product.Code}_image.png";

                imageDictionary.Add(imageProductName, product.Image);

                product.Image = imageProductName;
            }

            if (product.DataVersion.IsNullOrEmpty())
            {
                var productCheck = await this.productRepository.CheckProductCode(product.Code);
                if (productCheck is not null)
                {
                    throw new DomainException("", "Code đã tồn tại");
                }

                //create
                int productId = await this.productRepository.CreateProduct(product);
                idReturn = productId;

                //insert product color
                foreach (var productColor in request.ProductColors)
                {
                    productColor.ProductId = productId;

                    if (this.IsBase64String(productColor.Image))
                    {
                        string imageColorName = $"{product.Code}_{productColor.ColorId}_image.png";

                        imageDictionary.Add(imageColorName, productColor.Image);

                        productColor.Image = imageColorName;
                    }

                    await this.productRepository.CreateProductColor(productColor);
                }

                //insert product size
                foreach (var productSize in request.ProductSizes)
                {
                    productSize.ProductId = productId;

                    await this.productRepository.CreateProductSize(productSize);
                }

                //insert product detail
                foreach (var productDetail in request.ProductDetails)
                {
                    productDetail.ProductId = productId;
                    await this.productRepository.CreateProductDetail(productDetail);
                }
            }
            else
            {
                idReturn = request.Product.Id;
                //update
                await this.productRepository.UpdateProduct(product);

                await this.productRepository.HardDeleteProductColor(product.Id);

                foreach (var item in request.ProductColors)
                {
                    item.ProductId = product.Id;

                    if (this.IsBase64String(item.Image))
                    {
                        string imageColorName = $"{product.Code}_{item.ColorId}_image.png";

                        imageDictionary.Add(imageColorName, item.Image);

                        item.Image = imageColorName;
                    }

                    await this.productRepository.CreateProductColor(item);
                }

                await this.productRepository.HardDeleteProductSize(product.Id);

                foreach (var item in request.ProductSizes)
                {
                    item.ProductId = product.Id;
                    await this.productRepository.CreateProductSize(item);
                }

                var oldData = await this.productRepository.GetProductDetail(product.Id);

                foreach (var item in oldData.Where(o=> !request.ProductDetails.Exists(x=>x.Id == o.Id)))
                {
                    await this.productRepository.DeleteProductDetail(item.Id);
                }

                foreach (var item in request.ProductDetails)
                {
                    item.ProductId = product.Id;
                    if (item.DataVersion.IsNullOrEmpty())
                    {
                        await this.productRepository.CreateProductDetail(item);
                    }
                    else
                    {
                        var oldItem = oldData.FirstOrDefault(x => x.Id == item.Id);
                        item.Quantity = item.ImportQuantity + oldItem?.Quantity ?? 0;
                        item.ActualQuantity = item.ImportQuantity + oldItem?.ActualQuantity ?? 0;
                        await this.productRepository.UpdateProductDetail(item);
                    }
                }
            }

            foreach (var item in imageDictionary)
            {
                var imageBytes = Convert.FromBase64String(item.Value);
                using var image = Image.Load(imageBytes);
                await image.SaveAsync(@$"{this.configuration["ImagePath"]}{item.Key}", CancellationToken.None);
            }


            ////insert product
            //var createProductParam = new ProductInfo
            //{
            //    Code = request.Code,
            //    Name = request.Name,
            //    ClassificationId = request.ClassificationId,
            //    MaterialId = request.MaterialId,
            //    SupplierId = request.SupplierId,
            //    TrademarkId = request.TrademarkId,
            //    OriginId = request.OriginId,
            //    Description = request.Description
            //};

            //int productId = await this.productRepository.CreateProduct(createProductParam);

            ////insert image/ product image

            //foreach (string image in request.ProductImages)
            //{
            //    await this.imageRepository.CreateImage(productId, image);
            //}

            ////insert product color

            //var colorDictionary = new Dictionary<int, int>();

            //foreach (var productColor in request.ProductColors)
            //{
            //    int imageId = await this.imageRepository.CreateImage(productId, productColor.Image);

            //    int productColorId = await this.productRepository.CreateProductColor(productId, productColor.ColorId, imageId);

            //    colorDictionary.TryAdd(productColor.ColorId, productColorId);
            //}

            ////insert product size

            //foreach (var productSize in request.ProductDetails.DistinctBy(x=>x.SizeId))
            //{
            //    await this.productRepository.CreateProductSize(productId, productSize.SizeId);
            //}

            ////insert product detail
            //foreach (var productDetail in request.ProductDetails)
            //{
            //    if (!colorDictionary.TryGetValue(productDetail.ColorId, out int colorId))
            //    {
            //        throw new ArgumentNullException();
            //    }

            //    productDetail.ProductId = productId;
            //    productDetail.ColorId = colorId;
            //    await this.productRepository.CreateProductDetail(productDetail);
            //}

            scope.Complete();

            return new CreateProductCommandResult(idReturn);
        }

        public bool IsBase64String(string base64)
        {
            var buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int _);
        }
    }
}