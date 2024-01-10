using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Put
{
    public class ChangePasswordRequestModel
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ReNewPassword { get; set; }
    }

    public class ChangePasswordRequestModelValidator : AbstractValidator<ChangePasswordRequestModel>
    {
        public ChangePasswordRequestModelValidator()
        {
            this.RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage($"{nameof(ChangePasswordRequestModel.OldPassword)} không thể trống");
            this.RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage($"{nameof(ChangePasswordRequestModel.NewPassword)} không thể trống")
                .MinimumLength(8)
                .WithMessage($"{nameof(ChangePasswordRequestModel.NewPassword)} cần tối thiểu 8 ký tự");
            this.RuleFor(x => x.ReNewPassword)
                .NotEmpty()
                .WithMessage($"{nameof(ChangePasswordRequestModel.ReNewPassword)} không thể trống");
            this.RuleFor(x => x)
                .Must(x => x.ReNewPassword == x.NewPassword)
                .WithMessage("Mật khẩu mới không giống nhau")
                .Must(x=>x.NewPassword != x.OldPassword)
                .WithMessage("Mật khẩu mới nhật trùng với mật khẩu cũ");
        }
    }
}
