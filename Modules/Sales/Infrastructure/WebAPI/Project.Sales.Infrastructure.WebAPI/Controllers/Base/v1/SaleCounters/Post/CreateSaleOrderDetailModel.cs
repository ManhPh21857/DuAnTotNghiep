using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Post
{
    public class CreateSaleOrderDetailModel
    {
        public OrderModel1 Order { get; set; }
        public IEnumerable<OrderSaleDetailModel> Orderdetails { get; set; }
        public CreateSaleOrderDetailModel(IEnumerable<OrderSaleDetailModel> orderdetails)
        {
            this.Orderdetails = orderdetails;
        }
    }
    public class CreateOrderDetailModelValidator1 : AbstractValidator<CreateSaleOrderDetailModel>
    {
        public CreateOrderDetailModelValidator1(
            IValidator<OrderModel1> orderModelValidator,
            IValidator<OrderSaleDetailModel> cartDetailModelValidator
        )
        {
            this.RuleFor(x => x.Order).SetValidator(orderModelValidator!);
            this.RuleFor(x => x.Orderdetails).NotNull().NotEmpty();
            this.RuleForEach(x => x.Orderdetails).SetValidator(cartDetailModelValidator);
        }
    }
}
