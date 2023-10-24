
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post
{
    public class CreateCartdetailModel
    {
        public int? Cart_id { get; set; }
        public int? Product_detail_id { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }

    }
    public class CreateCartdetailModelValidator : AbstractValidator<CreateCartdetailModel>
    {
        public CreateCartdetailModelValidator()
        {
            RuleFor(v => v.Cart_id)
                .NotEmpty()
                .WithMessage($"{nameof(CreateCartdetailModel.Cart_id)} can not be empty");
            RuleFor(v => v.Product_detail_id)
               .NotEmpty()
               .WithMessage($"{nameof(CreateCartdetailModel.Product_detail_id)} can not be empty");
            RuleFor(v => v.Price)
               .NotEmpty()
               .WithMessage($"{nameof(CreateCartdetailModel.Price)} can not be empty");
            RuleFor(v => v.Quantity)
               .NotEmpty()
               .WithMessage($"{nameof(CreateCartdetailModel.Quantity)} can not be empty");
        }
    }
}
