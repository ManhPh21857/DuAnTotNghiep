using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Accuracy;

public class EmailAuthenticationRequestModel
{
    public string? Email { get; set; }
    public int Mode { get; set; }
}

public class EmailAuthenticationRequestModelValidator : AbstractValidator<EmailAuthenticationRequestModel>
{
    public EmailAuthenticationRequestModelValidator()
    {
        this.RuleFor(x => x.Email)
            .NotNull()
            .WithMessage($"{nameof(EmailAuthenticationRequestModel.Email)} không thể trống")
            .NotEmpty()
            .WithMessage($"{nameof(EmailAuthenticationRequestModel.Email)} không thể trống");
        //0-create, 1-forgot
        this.RuleFor(x => x.Mode).InclusiveBetween(0, 1);
    }
}
