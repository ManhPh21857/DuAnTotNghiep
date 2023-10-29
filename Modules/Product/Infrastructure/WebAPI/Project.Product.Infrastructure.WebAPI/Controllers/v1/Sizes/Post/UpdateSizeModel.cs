using FluentValidation;


namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Sizes.Post
{
    public class UpdateSizeModel
    {
        public int? Id { get; set; }
        public string? Size { get; set; }
        public byte[]? DataVersion { get; set; }
    }
    public class UpdateSizeModelValidator : AbstractValidator<UpdateSizeModel>
    {
        public UpdateSizeModelValidator()
        {
            this.RuleFor(x => x.Size)
                .NotNull()
                .WithMessage($"{nameof(UpdateSizeModel.Size)} must have a value")
                .NotEmpty()
                .WithMessage($"{nameof(UpdateSizeModel.Size)} must have a value");
        }
    }
}
