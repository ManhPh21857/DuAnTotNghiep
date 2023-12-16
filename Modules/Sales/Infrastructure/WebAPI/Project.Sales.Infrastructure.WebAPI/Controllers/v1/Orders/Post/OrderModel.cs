using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post
{
    public class OrderModel
    {
        public string? FullName { get; set; }
        public string? PhoneNUmber { get; set; }
        public string? Address { get; set; }
        public float? MerchandiseSubtotal { get; set; }
        public float? ShippingFee { get; set; }
        public float? ShippingDiscountSubtotal { get; set; }
        public float? VoucherApplied { get; set; }
        public float? OrderTotal { get; set; }
        public int? PaymentMethodId { get; set; }
    }

    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            this.RuleFor(x => x.Address).NotEmpty();
            this.RuleFor(x => x.MerchandiseSubtotal).NotNull();
            this.RuleFor(x => x.ShippingFee).NotNull();
            this.RuleFor(x => x.ShippingDiscountSubtotal).NotNull();
            this.RuleFor(x => x.VoucherApplied).NotNull();
            this.RuleFor(x => x.OrderTotal).NotNull();
            this.RuleFor(x => x.PaymentMethodId).NotNull();
        }
    }
}
