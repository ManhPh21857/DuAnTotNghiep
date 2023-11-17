using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post
{
    public class CreateCartdetailModel
    {
        public int CartId { get; set; }
        public int ProductDetailId { get; set; }
        public int Quantity { get; set; }
    }
    public class CreateCartdetailModelValidator : AbstractValidator<CreateCartdetailModel>
    {
        public CreateCartdetailModelValidator()
        {
            RuleFor(v => v.ProductDetailId)
                .NotEmpty()
                .WithMessage($"{nameof(CreateCartdetailModel.ProductDetailId)} can not be empty");
        }
    }


}
