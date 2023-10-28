using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Post
{
    public class UpdateTrademarkModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public byte[]? DataVersion { get; set; }
    }
    public class UpdateTrademarkModelValidator : AbstractValidator<UpdateTrademarkModel>
    {
        public UpdateTrademarkModelValidator()
        {
            RuleFor(v => v.Name)
                .NotNull()
                .WithMessage($"{nameof(UpdateTrademarkModel.Name)} must have a value")
                .NotEmpty()
                .WithMessage($"{nameof(UpdateTrademarkModel.Name)} must have a value");
        }
    }
}
