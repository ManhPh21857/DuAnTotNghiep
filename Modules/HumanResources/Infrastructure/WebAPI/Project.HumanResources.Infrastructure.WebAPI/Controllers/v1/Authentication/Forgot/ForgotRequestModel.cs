using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Forgot
{
    public class ForgotRequestModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class ForgotRequestModelValidator : AbstractValidator<ForgotRequestModel>
    {
        public ForgotRequestModelValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty();

            RuleFor(c => c.Code)
                .NotEmpty();

            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(c => c)
                .Must(x => x.RePassword == x.Password);
        }
    }
}
