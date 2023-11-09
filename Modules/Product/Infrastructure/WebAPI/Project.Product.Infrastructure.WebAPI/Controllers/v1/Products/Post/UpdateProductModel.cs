using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post
{
    public class UpdateProductModel
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? ClassificationId { get; set; }
        public int? MaterialId { get; set; }
        public int? SupplierId { get; set; }
        public int? TrademarkId { get; set; }
        public int? OriginId { get; set; }
        public string? Description { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateProductModelValidator : AbstractValidator<UpdateProductModel>
    {
        public UpdateProductModelValidator()
        {
            this.RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Image)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.ClassificationId)
                .NotNull();

            this.RuleFor(x => x.MaterialId)
                .NotNull();

            this.RuleFor(x => x.SupplierId)
                .NotNull();

            this.RuleFor(x => x.TrademarkId)
                .NotNull();

            this.RuleFor(x => x.OriginId)
                .NotNull();

            this.RuleFor(x => x.Description)
                .NotNull();
        }
    }
}