using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class UpdateProductRequestModel
    {
        public UpdateProductModel? Product { get; set; }
        public List<UpdateProductColorModel>? ProductColors { get; set; }
        public List<UpdateProductSizeModel>? ProductSizes { get; set; }
        public List<UpdateProductDetailModel>? ProductDetails { get; set; }
    }

    public class UpdateProductRequestModelValidator : AbstractValidator<UpdateProductRequestModel>
    {
        public UpdateProductRequestModelValidator(
            IValidator<UpdateProductModel> updateProductModelValidator,
            IValidator<UpdateProductColorModel> updateProductColorModelValidator,
            IValidator<UpdateProductSizeModel> updateProductSizeModelValidator,
            IValidator<UpdateProductDetailModel> updateProductDetailModelValidator
        )
        {
            this.RuleFor(x => x.Product)
                .NotNull();

            this.RuleFor(x => x.Product)
                .SetValidator(updateProductModelValidator!);

            this.RuleFor(x => x.ProductColors)
                .NotNull();

            this.RuleForEach(x => x.ProductColors)
                .SetValidator(updateProductColorModelValidator);

            this.RuleFor(x => x.ProductSizes)
                .NotNull();


            this.RuleForEach(x => x.ProductSizes)
                .SetValidator(updateProductSizeModelValidator);

            this.RuleFor(x => x.ProductDetails)
                .NotNull();

            this.RuleForEach(x => x.ProductDetails)
                .SetValidator(updateProductDetailModelValidator);
        }
    }
}