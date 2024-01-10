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
        this.RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage($"{nameof(LoginRequestModel.UserName)} không thể trống");

        this.RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage($"{nameof(LoginRequestModel.Password)} không thể trống");
    }
}