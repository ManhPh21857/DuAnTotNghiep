using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Put
{
    public class UpdateCustomerModel
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Image { get; set; }
        public int? Sex { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateCustomerModelValidator : AbstractValidator<UpdateCustomerModel>
    {
        public UpdateCustomerModelValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNull()
                .WithMessage($"{nameof(UpdateCustomerModel.Id)} không thể trống");

            this.RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.Username)} không thể trống");

            this.RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.LastName)} không thể trống");

            this.RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.FirstName)} không thể trống");

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.PhoneNumber)} không thể trống");

            this.RuleFor(x => x.Birthday)
                .NotNull()
                .WithMessage($"{nameof(UpdateCustomerModel.Birthday)} không thể trống");

            this.RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.Image)} không thể trống");

            this.RuleFor(x => x.Sex)
                .NotNull()
                .WithMessage($"{nameof(UpdateCustomerModel.Sex)} không thể trống")
                .InclusiveBetween(0, 1);
        }
    }
}
