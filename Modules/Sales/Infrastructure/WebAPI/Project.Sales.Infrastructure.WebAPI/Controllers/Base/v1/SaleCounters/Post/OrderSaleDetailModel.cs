using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class OrderSaleDetailModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDetailValidator1 : AbstractValidator<OrderSaleDetailModel>
    {
        public OrderDetailValidator1()
        {
            this.RuleFor(x => x.ProductId)
                .NotNull()
                .WithMessage($"{nameof(OrderSaleDetailModel.ProductId)} không thể trống");

            this.RuleFor(x => x.ColorId)
                .NotNull()
                .WithMessage($"{nameof(OrderSaleDetailModel.ColorId)} không thể trống");

            this.RuleFor(x => x.SizeId)
                .NotNull()
                .WithMessage($"{nameof(OrderSaleDetailModel.SizeId)} không thể trống");

            this.RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage($"{nameof(OrderSaleDetailModel.ProductName)} không thể trống")
                .MaximumLength(500);

            this.RuleFor(x => x.Price)
                .NotNull()
                .WithMessage($"{nameof(OrderSaleDetailModel.Price)} không thể trống");

            this.RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage($"{nameof(OrderSaleDetailModel.Quantity)} không thể trống");
        }
    }
}
