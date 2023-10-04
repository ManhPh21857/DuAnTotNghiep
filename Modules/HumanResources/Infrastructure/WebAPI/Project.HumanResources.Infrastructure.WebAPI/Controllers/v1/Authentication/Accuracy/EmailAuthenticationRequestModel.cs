using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Authentication.Accuracy;

public class EmailAuthenticationRequestModel
{
    public string? Email { get; set; }
}

public class EmailAuthenticationRequestModelValidator : AbstractValidator<EmailAuthenticationRequestModel>
{
    public EmailAuthenticationRequestModelValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage($"{nameof(EmailAuthenticationRequestModel.Email)} must have a value")
            .NotEmpty()
            .WithMessage($"{nameof(EmailAuthenticationRequestModel.Email)} must have a value");
    }
}