using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Register;

public class RegisterRequestModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? RePassword { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator()
    {
        RuleFor(v => v.UserName)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.UserName)} can not be empty")
            .MinimumLength(8)
            .WithMessage($"{nameof(RegisterRequestModel.UserName)} needs at least 8 characters")
            .MaximumLength(50)
            .WithMessage($"{nameof(RegisterRequestModel.UserName)} too long");

        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.Password)} can not be empty")
            .MinimumLength(6)
            .WithMessage($"{nameof(RegisterRequestModel.Password)} needs at least 8 characters")
            .MaximumLength(50)
            .WithMessage($"{nameof(RegisterRequestModel.Password)} too long");

        RuleFor(v => v.RePassword)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.RePassword)} can not be empty");

        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.Email)} can not be empty");

        RuleFor(v => v)
            .Must(m => m.RePassword == m.Password)
            .WithMessage(
                $"{nameof(RegisterRequestModel.RePassword)} is not the same as {nameof(RegisterRequestModel.Password)}");

        RuleFor(v => v.FirstName)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.FirstName)} can not be empty");

        RuleFor(v => v.LastName)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.LastName)} can not be empty");
    }
}