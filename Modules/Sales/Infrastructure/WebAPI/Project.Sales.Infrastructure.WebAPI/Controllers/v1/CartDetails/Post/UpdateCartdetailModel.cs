
using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post
{
    public class UpdateCartdetailModel
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public byte[]? DataVersion { get; set; }


    }
    public class UpdateCartdetailModelValidator : AbstractValidator<UpdateCartdetailModel>
    {
        public UpdateCartdetailModelValidator()
        {

            //RuleFor(v => v.Price)
            //   .NotEmpty()
            //   .WithMessage($"{nameof(UpdateCartdetailModel.Price)} mush have a value")
            //   .NotNull()
            //   .WithMessage($"{nameof(UpdateCartdetailModel.Price)} mush have a value");
            //RuleFor(v => v.Quantity)
            //   .NotEmpty()
            //   .WithMessage($"{nameof(UpdateCartdetailModel.Quantity)} mush have a value")
            //   .NotNull()
            //   .WithMessage($"{nameof(UpdateCartdetailModel.Quantity)} mush have a value");
        }
    }
}
