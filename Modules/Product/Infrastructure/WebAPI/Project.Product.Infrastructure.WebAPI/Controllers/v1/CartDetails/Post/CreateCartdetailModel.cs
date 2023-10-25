
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Post
{
    public class CreateCartdetailModel
    {
        public int? Cartid { get; set; }
        public int? Productdetailid { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }

    }
    public class CreateCartdetailModelValidator : AbstractValidator<CreateCartdetailModel>
    {
        public CreateCartdetailModelValidator()
        {
            RuleFor(v => v.Cartid)
                .NotEmpty()
                .WithMessage($"{nameof(CreateCartdetailModel.Cartid)} can not be empty");
            RuleFor(v => v.Productdetailid)
               .NotEmpty()
               .WithMessage($"{nameof(CreateCartdetailModel.Productdetailid)} can not be empty");
            RuleFor(v => v.Price)
               .NotEmpty()
               .WithMessage($"{nameof(CreateCartdetailModel.Price)} can not be empty");
            RuleFor(v => v.Quantity)
               .NotEmpty()
               .WithMessage($"{nameof(CreateCartdetailModel.Quantity)} can not be empty");
        }
    }
}
