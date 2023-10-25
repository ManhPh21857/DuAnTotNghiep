
using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.CartDetails.Delete
{
    public class DeleteCartdetailModel
    {
        public int? Cartid { get; set; }
        public int? Productdetailid { get; set; }
    }
    public class DeleteCartdetailModelValidator : AbstractValidator<DeleteCartdetailModel>
    {
        public DeleteCartdetailModelValidator()
        {
            RuleFor(v => v.Cartid)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteCartdetailModel.Cartid)} can not be empty");
            RuleFor(v => v.Productdetailid)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteCartdetailModel.Productdetailid)} can not be empty");
        }
    }
}
