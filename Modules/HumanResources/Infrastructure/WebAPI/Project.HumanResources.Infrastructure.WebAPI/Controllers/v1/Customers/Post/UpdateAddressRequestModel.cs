using FluentValidation;
using System.Text.RegularExpressions;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Post
{
    public class UpdateAddressRequestModel
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? AddressDetail { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateAddressRequestModelValidator : AbstractValidator<UpdateAddressRequestModel>
    {
        private readonly Regex regex = new(@"(84|0[3|5|7|8|9])+([0-9]{8})\b");
        public UpdateAddressRequestModelValidator()
        {
            this.RuleFor(x => x.CustomerName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateAddressRequestModel.CustomerName)} không thể trống")
                .MaximumLength(100)
                .WithMessage($"{nameof(UpdateAddressRequestModel.CustomerName)} tối đa 100 ký tự");

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateAddressRequestModel.PhoneNumber)} không thể trống")
                .Must(x => this.regex.IsMatch(x))
                .WithMessage($"{nameof(UpdateAddressRequestModel.PhoneNumber)} sai định dạng");

            this.RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateAddressRequestModel.City)} không thể trống")
                .MaximumLength(50);

            this.RuleFor(x => x.District)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateAddressRequestModel.District)} không thể trống")
                .MaximumLength(50);

            this.RuleFor(x => x.Commune)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateAddressRequestModel.Commune)} không thể trống")
                .MaximumLength(50);

            this.RuleFor(x => x.AddressDetail)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateAddressRequestModel.AddressDetail)} không thể trống")
                .MaximumLength(500);
        }
    }
}
