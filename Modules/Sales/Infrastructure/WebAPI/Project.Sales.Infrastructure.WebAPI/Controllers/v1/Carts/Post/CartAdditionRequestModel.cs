using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Post
{
    public class CartAdditionRequestModel
    {
        public int? ProductDetailId { get; set; }
        public int? Quantity { get; set; }
    }

    public class CartAdditionRequestModelValidator : AbstractValidator<CartAdditionRequestModel>
    {
        public CartAdditionRequestModelValidator()
        {
            this.RuleFor(x => x.ProductDetailId)
                .NotNull()
                .WithMessage($"{nameof(CartAdditionRequestModel.ProductDetailId)} không thể trống");

            this.RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage($"{nameof(CartAdditionRequestModel.Quantity)} không thể trống")
                .GreaterThan(0);
        }
    }
}
