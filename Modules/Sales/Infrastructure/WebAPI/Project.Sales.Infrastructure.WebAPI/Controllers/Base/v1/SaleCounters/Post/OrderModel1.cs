using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class OrderModel1
    {

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public float VoucherApplied { get; set; }
        public float OrderTotal { get; set; }
        public int VoucherId { get; set; }
        public float MerchandiseSubtotal { get; set; }
        public int PaymentMethodId { get; set; }
        public int? Status { get; set; }
    }

    public class OrderModelValidator1 : AbstractValidator<OrderModel1>
    {
        public OrderModelValidator1()
        {
            this.RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage($"{nameof(OrderModel1.FullName)} không thể trống")
                .MaximumLength(100);

            this.RuleFor(x => x.MerchandiseSubtotal)
                .NotNull()
                .WithMessage($"{nameof(OrderModel1.MerchandiseSubtotal)} không thể trống");

            this.RuleFor(x => x.VoucherApplied)
                .NotNull()
                .WithMessage($"{nameof(OrderModel1.VoucherApplied)} không thể trống");

            this.RuleFor(x => x.OrderTotal)
                .NotNull()
                .WithMessage($"{nameof(OrderModel1.OrderTotal)} không thể trống");

        }
    }
}
