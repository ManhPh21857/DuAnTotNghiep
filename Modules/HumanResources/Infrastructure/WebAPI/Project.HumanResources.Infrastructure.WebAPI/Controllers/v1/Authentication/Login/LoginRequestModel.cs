using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Login;

public class LoginRequestModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestModelValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage($"{nameof(LoginRequestModel.UserName)} must has a value");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage($"{nameof(LoginRequestModel.Password)} must has a value");
    }
}