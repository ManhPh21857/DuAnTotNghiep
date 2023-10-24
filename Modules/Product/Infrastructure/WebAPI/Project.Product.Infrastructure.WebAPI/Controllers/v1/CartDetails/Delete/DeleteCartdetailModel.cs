
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Delete
{
    public class DeleteCartdetailModel
    {
        public int? Cart_id { get; set; }
        public int? Product_detail_id { get; set; }
    }
    public class DeleteCartdetailModelValidator : AbstractValidator<DeleteCartdetailModel>
    {
        public DeleteCartdetailModelValidator()
        {
            RuleFor(v => v.Cart_id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteCartdetailModel.Cart_id)} can not be empty");
            RuleFor(v => v.Product_detail_id)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteCartdetailModel.Product_detail_id)} can not be empty");
        }
    }
}
