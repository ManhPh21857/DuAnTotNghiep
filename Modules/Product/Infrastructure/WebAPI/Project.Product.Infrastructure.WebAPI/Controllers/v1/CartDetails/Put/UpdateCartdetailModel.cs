
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Put
{
    public class UpdateCartdetailModel
    {
        public int? Cart_id { get; set; }
        public int? Product_detail_id { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
    }
    public class UpdateCartdetailModelValidator : AbstractValidator<UpdateCartdetailModel>
    {
        public UpdateCartdetailModelValidator()
        {
            RuleFor(v => v.Cart_id)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Cart_id)} can not be empty");


            RuleFor(v => v.Product_detail_id)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Product_detail_id)} can not be empty");

            RuleFor(v => v.Price)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Price)} can not be empty");

            RuleFor(v => v.Quantity)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCartdetailModel.Quantity)} can not be empty");

        }
    }
}
