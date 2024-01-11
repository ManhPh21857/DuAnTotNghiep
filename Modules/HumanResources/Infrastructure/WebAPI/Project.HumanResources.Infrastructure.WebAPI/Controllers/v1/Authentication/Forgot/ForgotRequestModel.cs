using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Forgot
{
    public class ForgotRequestModel
    {
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? Password { get; set; }
        public string? RePassword { get; set; }
    }

    public class ForgotRequestModelValidator : AbstractValidator<ForgotRequestModel>
    {
        public ForgotRequestModelValidator()
        {
            this.RuleFor(c => c.Email)
                .NotEmpty()
                .WithMessage($"{nameof(ForgotRequestModel.Email)} không thể trống");

            this.RuleFor(c => c.Code)
                .NotEmpty()
                .WithMessage($"{nameof(ForgotRequestModel.Code)} không thể trống");

            this.RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage($"{nameof(ForgotRequestModel.Password)} không thể trống")
                .MinimumLength(8)
                .WithMessage($"{nameof(ForgotRequestModel.Password)} tối thiểu 8 ký tự")
                .MaximumLength(50)
                .WithMessage($"{nameof(ForgotRequestModel.Password)} tối đa 50 ký tự");

            this.RuleFor(c => c)
                .Must(x => x.RePassword == x.Password)
                .WithMessage($"mật khẩu nhập lại không khớp");
        }
    }
}
