using FluentValidation;
using System.Text.RegularExpressions;

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
        private readonly Regex regex = new(@"(84|0[3|5|7|8|9])+([0-9]{8})\b");
        public UpdateCustomerModelValidator()
        {

            this.RuleFor(x => x.Id)
                .NotNull()
                .WithMessage($"{nameof(UpdateCustomerModel.Id)} không thể trống");

            this.RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.Username)} không thể trống")
                .MaximumLength(50);

            this.RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.LastName)} không thể trống")
                .MaximumLength(100);

            this.RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.FirstName)} không thể trống")
                .MaximumLength(100);

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateCustomerModel.PhoneNumber)} không thể trống")
                .MaximumLength(10)
                .WithMessage($"{nameof(UpdateCustomerModel.PhoneNumber)} tối đa 10 ký tự")
                .Must(x => this.regex.IsMatch(x));

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
