using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class UpdateProductDetailModel
    {
        public int? Id { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public float? ImportPrice { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateProductDetailModelValidator : AbstractValidator<UpdateProductDetailModel>
    {
        public UpdateProductDetailModelValidator()
        {
            this.RuleFor(x => x.ColorId)
                .NotNull();

            this.RuleFor(x => x.SizeId)
                .NotNull();

            this.RuleFor(x => x.ImportPrice)
                .NotNull();

            this.RuleFor(x => x.Price)
                .NotNull();

            this.RuleFor(x => x.Quantity)
                .NotNull();
        }
    }
}