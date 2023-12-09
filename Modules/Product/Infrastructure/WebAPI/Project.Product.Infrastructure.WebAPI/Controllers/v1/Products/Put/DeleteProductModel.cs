using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Put
{
    public class DeleteProductModel
    {
        public int? Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class DeleteProductModelValidator : AbstractValidator<DeleteProductModel>
    {
        public DeleteProductModelValidator()
        {
            this.RuleFor(c => c.Id).NotNull();
        }
    }
}
