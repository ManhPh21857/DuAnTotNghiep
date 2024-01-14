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
            this.RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("tên không thể trống")
                .MaximumLength(500);

            this.RuleFor(x => x.VoucherType)
                .NotNull()
                .InclusiveBetween(1, 2);

            this.RuleFor(x => x.MinimumPrice)
                .NotNull()
                .WithMessage("giá trị tối thiểu không thể trống")
                .GreaterThan(0);

            this.RuleFor(x => x.Discount)
                .NotNull()
                .WithMessage("giảm giá không thể trống")
                .GreaterThan(0);

            this.RuleFor(x => x.DiscountType)
                .NotNull()
                .WithMessage("sdfsdf");

            this.RuleFor(x => x.ApplyPeriodStart)
                .NotNull()
                .WithMessage("ngày bắt đầu áp dụng không thể trống");

            this.RuleFor(x => x.ApplyPeriodEnd)
                .NotNull()
                .WithMessage("ngày hết hạn không thể trống");

            this.RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("vui lòng nhập số lượng");

            this.RuleFor(x => x.DiscountType)
                .InclusiveBetween(1, 2);

            this.RuleFor(x => x)
                .Must(x => (x.MaximumDiscount > 0 && x.Discount <= 100) || x.DiscountType == 2)
                .WithMessage("Với phiếu giảm giá theo phần trăm, hãy đảm bảo rằng giảm giá không vượt quá 100% và giảm giá tối đa là bắt buộc (>0)")
                .Must(DiscountEnable)
                .WithMessage("giảm giá không được vượt mức giá trị đơn hàng");

            bool DiscountEnable(UpdateVoucherRequestModel input) 
            {
                if (input.DiscountType == 1)
                {
                    if (input.MaximumDiscount > input.MinimumPrice)
                    {
                        return false;
                    }
                }
                else
                {
                    if (input.Discount > input.MinimumPrice)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
