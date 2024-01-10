using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Register;

public class RegisterRequestModel
{
    public string? Email { get; set; }
    public string? Code { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? RePassword { get; set; }
}

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator()
    {
        this.RuleFor(v => v.UserName)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.UserName)} không thể trống")
            .MinimumLength(8)
            .WithMessage($"{nameof(RegisterRequestModel.UserName)} phải có tối thiểu 8 ký tự")
            .MaximumLength(50)
            .WithMessage($"{nameof(RegisterRequestModel.UserName)} quá dài, tối đa 50 ký tự");

        this.RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.Password)} không thể trống")
            .MinimumLength(6)
            .WithMessage($"{nameof(RegisterRequestModel.Password)} phải có tối thiểu 8 ký tự")
            .MaximumLength(50)
            .WithMessage($"{nameof(RegisterRequestModel.Password)} quá dài, tối đa 50 ký tự");

        this.RuleFor(v => v.RePassword)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.RePassword)} không thể trống");

        this.RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.Email)} không thể trống");

        this.RuleFor(v => v.Code)
            .NotEmpty()
            .WithMessage($"{nameof(RegisterRequestModel.Code)} không thể trống");

        this.RuleFor(v => v)
            .Must(m => m.RePassword == m.Password)
            .WithMessage(
                $"{nameof(RegisterRequestModel.RePassword)} không trùng {nameof(RegisterRequestModel.Password)}");
    }
}