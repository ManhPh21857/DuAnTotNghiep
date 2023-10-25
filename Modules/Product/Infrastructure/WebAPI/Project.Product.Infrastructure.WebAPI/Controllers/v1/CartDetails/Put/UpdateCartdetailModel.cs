
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Put
{
    public class UpdateCartdetailModel
    {
        public int? Cartid { get; set; }
        public int? Productdetailid { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
    }
    public class UpdateCartdetailModelValidator : AbstractValidator<UpdateCartdetailModel>
    {
        public UpdateCartdetailModelValidator()
        {
            RuleFor(v => v.Cartid)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Cartid)} can not be empty");


            RuleFor(v => v.Productdetailid)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Productdetailid)} can not be empty");

            RuleFor(v => v.Price)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Price)} can not be empty");

            RuleFor(v => v.Quantity)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Quantity)} can not be empty");

        }
    }
}
