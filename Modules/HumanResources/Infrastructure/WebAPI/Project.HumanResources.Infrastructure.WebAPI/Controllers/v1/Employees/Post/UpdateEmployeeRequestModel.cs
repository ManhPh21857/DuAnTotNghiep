using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Employees.Post
{
    public class UpdateEmployeeRequestModel
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Sex { get; set; }
        public string? PhoneNumber { get; set; }
        public byte[]? EmployeeDataVersion { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public byte[]? UserDataVersion { get; set; }
        public IEnumerable<int>? Roles { get; set; }
    }

    public class UpdateEmployeeRequestModelValidator : AbstractValidator<UpdateEmployeeRequestModel>
    {
        public UpdateEmployeeRequestModelValidator()
        {
            this.RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Email)} không thể trống");

            this.RuleFor(x => x.Username)
                .MinimumLength(8)
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Username)} cần tối thiểu 8 ký tự")
                .MaximumLength(50);

            this.RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Password)} cần tối thiểu 8 ký tự")
                .MaximumLength(50);

            this.RuleFor(x => x.Id);

            this.RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.FirstName)} không thể trống")
                .MaximumLength(100);

            this.RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.LastName)} không thể trống")
                .MaximumLength(100);

            this.RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Image)} không thể trống");

            this.RuleFor(x => x.Address)
                .MaximumLength(500);

            this.RuleFor(x => x.Birthday)
                .NotNull()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Birthday)} không thể trống")
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Birthday)} không thể trống");

            this.RuleFor(x => x.Sex)
                .NotNull()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Sex)} không thể trống");

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.PhoneNumber)} không thể trống")
                .MaximumLength(10);

            this.RuleFor(x => x.Roles)
                .NotEmpty()
                .WithMessage($"{nameof(UpdateEmployeeRequestModel.Roles)} không thể trống");
        }
    }
}
