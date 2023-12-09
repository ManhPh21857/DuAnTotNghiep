using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Carts.Put
{
    public class DeleteCartDetailRequestModel
    {
        public IEnumerable<DeleteCartDetailModel> CartDetails { get; set; }

        public DeleteCartDetailRequestModel()
        {
            this.CartDetails = new List<DeleteCartDetailModel>();
        }
    }

    public class DeleteCartDetailRequestModelValidator : AbstractValidator<DeleteCartDetailRequestModel>
    {
        public DeleteCartDetailRequestModelValidator(IValidator<DeleteCartDetailModel> validator)
        {
            this.RuleForEach(x => x.CartDetails).SetValidator(validator);
        }
    }
}
