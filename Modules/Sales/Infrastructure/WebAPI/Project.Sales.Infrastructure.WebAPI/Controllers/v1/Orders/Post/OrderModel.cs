using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post
{
    public class OrderModel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public float? MerchandiseSubtotal { get; set; }
        public float? ShippingFee { get; set; }
        public float? ShippingDiscountSubtotal { get; set; }
        public int? VoucherId { get; set; }
        public float? VoucherApplied { get; set; }
        public float? OrderTotal { get; set; }
        public int? PaymentMethodId { get; set; }
    }

    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            this.RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage($"{nameof(OrderModel.FullName)} không thể trống")
                .MaximumLength(100);

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage($"{nameof(OrderModel.PhoneNumber)} không thể trống")
                .MaximumLength(10);

            this.RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage($"{nameof(OrderModel.Address)} không thể trống")
                .MaximumLength(500);

            this.RuleFor(x => x.MerchandiseSubtotal)
                .NotNull()
                .WithMessage($"{nameof(OrderModel.MerchandiseSubtotal)} không thể trống");

            this.RuleFor(x => x.ShippingFee)
                .NotNull()
                .WithMessage($"{nameof(OrderModel.ShippingFee)} không thể trống");

            this.RuleFor(x => x.ShippingDiscountSubtotal)
                .NotNull()
                .WithMessage($"{nameof(OrderModel.ShippingDiscountSubtotal)} không thể trống");

            this.RuleFor(x => x.VoucherApplied)
                .NotNull()
                .WithMessage($"{nameof(OrderModel.VoucherApplied)} không thể trống");

            this.RuleFor(x => x.OrderTotal)
                .NotNull()
                .WithMessage($"{nameof(OrderModel.OrderTotal)} không thể trống");

            this.RuleFor(x => x.PaymentMethodId)
                .NotNull()
                .WithMessage($"{nameof(OrderModel.PaymentMethodId)} không thể trống");

        }
    }
}
