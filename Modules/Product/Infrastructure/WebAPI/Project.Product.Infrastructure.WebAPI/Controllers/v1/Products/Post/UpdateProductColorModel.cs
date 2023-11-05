using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class UpdateProductColorModel
    {
        public int? ColorId { get; set; }
        public string? Image { get; set; }
    }

    public class UpdateProductColorModelValidator : AbstractValidator<UpdateProductColorModel>
    {
        public UpdateProductColorModelValidator()
        {
            this.RuleFor(x => x.ColorId)
                .NotNull();

            this.RuleFor(x => x.Image)
                .NotNull()
                .NotEmpty();
        }

    }
}
