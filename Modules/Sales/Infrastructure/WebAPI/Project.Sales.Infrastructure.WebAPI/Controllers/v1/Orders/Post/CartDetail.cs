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
            this.RuleFor(x => x.ProductId).NotNull();
            this.RuleFor(x => x.ColorId).NotNull();
            this.RuleFor(x => x.SizeId).NotNull();
            this.RuleFor(x => x.ProductName).NotEmpty();
            this.RuleFor(x => x.Price).NotNull();
            this.RuleFor(x => x.Quantity).NotNull();
        }
    }
}
