using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Post
{
    public class UpdateVoucherRequestModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? VoucherType { get; set; }
        public float? MinimumPrice { get; set; }
        public float? Discount { get; set; }
        public int? DiscountType { get; set; }
        public float? MaximumDiscount { get; set; }
        public DateTime? ApplyPeriodStart { get; set; }
        public DateTime? ApplyPeriodEnd { get; set; }
        public int? Quantity { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateVoucherRequestModelValidator : AbstractValidator<UpdateVoucherRequestModel>
    {
        public UpdateVoucherRequestModelValidator()
        {
            this.RuleFor(x => x.Name).NotEmpty();
            this.RuleFor(x => x.VoucherType).NotNull().InclusiveBetween(1, 2);
            this.RuleFor(x => x.MinimumPrice).NotNull();
            this.RuleFor(x => x.Discount).NotNull();
            this.RuleFor(x => x.DiscountType).NotNull();
            this.RuleFor(x => x.ApplyPeriodStart).NotNull();
            this.RuleFor(x => x.ApplyPeriodEnd).NotNull();
            this.RuleFor(x => x.Quantity).NotNull();
            this.RuleFor(x => x.DiscountType).InclusiveBetween(1, 2);

            this.RuleFor(x => x)
                .Must(x => x.MaximumDiscount != null || x.DiscountType == 2)
                .WithMessage("Hãy nhập giá trị tối thiểu");
        }
    }
}
