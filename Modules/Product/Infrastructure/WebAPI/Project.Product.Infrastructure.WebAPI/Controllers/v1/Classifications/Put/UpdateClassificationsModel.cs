using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Put
{
    public class UpdateClassificationsModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
    public class UpdateClassificationsModelValidator : AbstractValidator<UpdateClassificationsModel>
    {
        public UpdateClassificationsModelValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateClassificationsModel.Name)} can not be empty")
                .NotNull()
                .WithMessage($"{nameof(UpdateClassificationsModel.Name)} can not null");

            RuleFor(v => v.Id)
                .NotNull()
                .WithMessage($"{nameof(UpdateClassificationsModel.Id)} can not null");

        }
    }
}
