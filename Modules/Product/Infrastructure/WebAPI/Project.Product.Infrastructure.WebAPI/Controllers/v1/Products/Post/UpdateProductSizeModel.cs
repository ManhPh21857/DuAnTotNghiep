using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class UpdateProductSizeModel
    {
        public int? SizeId { get; set; }
    }

    public class UpdateProductSizeModelValidator : AbstractValidator<UpdateProductSizeModel>
    {
        public UpdateProductSizeModelValidator()
        {
            this.RuleFor(x => x.SizeId)
                .NotNull();
        }
    }
}
