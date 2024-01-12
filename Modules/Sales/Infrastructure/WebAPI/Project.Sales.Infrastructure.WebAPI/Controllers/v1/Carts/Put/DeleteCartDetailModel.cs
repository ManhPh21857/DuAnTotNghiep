using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Put
{
    public class DeleteCartDetailModel
    {
        public int? ProductDetailId { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class DeleteCartDetailModelValidator : AbstractValidator<DeleteCartDetailModel>
    {
        public DeleteCartDetailModelValidator()
        {
            this.RuleFor(x => x.ProductDetailId)
                .NotNull()
                .WithMessage($"{nameof(DeleteCartDetailModel.ProductDetailId)} không thể trống");
        }
    }
}
