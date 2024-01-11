using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post
{
    public class CartDetail
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public string? ProductName { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class CartDetailValidator : AbstractValidator<CartDetail>
    {
        public CartDetailValidator()
        {
            this.RuleFor(x => x.ProductId)
                .NotNull()
                .WithMessage($"{nameof(CartDetail.ProductId)} không thể trống");

            this.RuleFor(x => x.ColorId)
                .NotNull()
                .WithMessage($"{nameof(CartDetail.ColorId)} không thể trống");

            this.RuleFor(x => x.SizeId)
                .NotNull()
                .WithMessage($"{nameof(CartDetail.SizeId)} không thể trống");

            this.RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage($"{nameof(CartDetail.ProductName)} không thể trống")
                .MaximumLength(500);

            this.RuleFor(x => x.Price)
                .NotNull()
                .WithMessage($"{nameof(CartDetail.Price)} không thể trống");

            this.RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage($"{nameof(CartDetail.Quantity)} không thể trống");
        }
    }
}
