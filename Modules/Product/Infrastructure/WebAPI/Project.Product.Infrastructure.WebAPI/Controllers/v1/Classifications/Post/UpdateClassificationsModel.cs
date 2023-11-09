using FluentValidation;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Classifications.Post
{
    public class UpdateClassificationsModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public byte[]? DataVersion { get; set; }
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

           

        }
    }
}
