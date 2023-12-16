using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post
{
    public class CreateOrderRequestModel
    {
        public OrderModel? Order { get; set; }
        public IEnumerable<CartDetail> CartDetails { get; set; }

        public CreateOrderRequestModel()
        {
            this.CartDetails = new List<CartDetail>();
        }
    }

    public class CreateOrderRequestModelValidator : AbstractValidator<CreateOrderRequestModel>
    {
        public CreateOrderRequestModelValidator(
            IValidator<OrderModel> orderModelValidator,
            IValidator<CartDetail> cartDetailModelValidator
        )
        {
            this.RuleFor(x => x.Order).SetValidator(orderModelValidator!);
            this.RuleFor(x => x.CartDetails).NotNull().NotEmpty();
            this.RuleForEach(x => x.CartDetails).SetValidator(cartDetailModelValidator);
        }
    }
}
