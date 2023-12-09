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
                .NotEmpty();

            this.RuleFor(x => x.Username)
                .MinimumLength(8);

            this.RuleFor(x => x.Password)
                .MinimumLength(8);

            this.RuleFor(x => x.Id);

            this.RuleFor(x => x.FirstName)
                .NotEmpty();

            this.RuleFor(x => x.LastName)
                .NotEmpty();

            this.RuleFor(x => x.Image)
                .NotEmpty();

            this.RuleFor(x => x.Address);

            this.RuleFor(x => x.Birthday)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Sex)
                .NotNull();

            this.RuleFor(x => x.PhoneNumber)
                .NotEmpty();

            this.RuleFor(x => x.Roles)
                .NotEmpty();
        }
    }
}
