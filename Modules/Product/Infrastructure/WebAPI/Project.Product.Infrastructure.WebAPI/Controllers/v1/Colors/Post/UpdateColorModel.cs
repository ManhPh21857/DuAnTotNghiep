using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Post
{
    public class UpdateColorModel
    {
        public int? Id { get; set; }
        public string? Color { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateColorModelValidator : AbstractValidator<UpdateColorModel>
    {
        public UpdateColorModelValidator()
        {
            this.RuleFor(x => x.Color)
                .NotNull()
                .WithMessage($"{nameof(UpdateColorModel.Color)} must have a value")
                .NotEmpty()
                .WithMessage($"{nameof(UpdateColorModel.Color)} must have a value");
        }
    }
}
