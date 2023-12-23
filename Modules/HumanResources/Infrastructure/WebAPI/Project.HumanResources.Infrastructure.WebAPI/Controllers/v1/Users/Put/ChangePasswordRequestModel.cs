using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Users.Put
{
    public class ChangePasswordRequestModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }

    public class ChangePasswordRequestModelValidator : AbstractValidator<ChangePasswordRequestModel>
    {
        public ChangePasswordRequestModelValidator()
        {
            this.RuleFor(x => x.OldPassword)
                .NotEmpty()
                .MinimumLength(8);
            this.RuleFor(x => x.NewPassword)
                .NotEmpty()
                .MinimumLength(8);
            this.RuleFor(x => x.ReNewPassword).NotEmpty();
            this.RuleFor(x => x)
                .Must(x => x.ReNewPassword == x.NewPassword)
                .WithMessage("Mật khẩu mới không giống nhau")
                .Must(x=>x.NewPassword != x.OldPassword)
                .WithMessage("Mật khẩu mới nhật trùng với mật khẩu cũ");
        }
    }
}
