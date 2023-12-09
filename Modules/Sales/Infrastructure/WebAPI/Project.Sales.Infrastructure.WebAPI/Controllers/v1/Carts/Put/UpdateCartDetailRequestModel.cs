using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Put
{
    public class UpdateCartDetailRequestModel
    {
        public int? ProductDetailId { get; set; }
        public int? ProductDetailIdNew { get; set; }
        public int? Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateCartDetailRequestModelValidator : AbstractValidator<UpdateCartDetailRequestModel>
    {
        public UpdateCartDetailRequestModelValidator()
        {
            this.RuleFor(x => x.ProductDetailId).NotNull();
            this.RuleFor(x => x.ProductDetailIdNew).NotNull();
            this.RuleFor(x => x.Quantity).NotNull();
        }
    }
}
