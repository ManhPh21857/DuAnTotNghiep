using FluentValidation;
namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Trademarks.Put
{
    public class UpdateTrademarkModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class UpdateTrademarkModelValidator : AbstractValidator<UpdateTrademarkModel>
    {
        public UpdateTrademarkModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateTrademarkModel.Name)} can not be empty");


            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateTrademarkModel.Id)} can not be empty");

        }
    }
}
