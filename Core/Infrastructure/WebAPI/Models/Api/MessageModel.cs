using FluentValidation;

namespace Project.Core.Infrastructure.WebAPI.Models.Api;

public class MessageModel
{
    public string? Code { get; set; }
    public object? Title { get; set; }
    public string? Field { get; set; }
    public string? Message { get; set; }
}

public class CreateMessageModelValidator : AbstractValidator<MessageModel>
{
    public void MessageCreateModelValidator()
    {
        this.RuleFor(x => x.Code);
        this.RuleFor(x => x.Title);
        this.RuleFor(x => x.Field);
        this.RuleFor(x => x.Message);
    }
}