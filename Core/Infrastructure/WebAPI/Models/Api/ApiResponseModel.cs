using FluentValidation;

namespace Project.Core.Infrastructure.WebAPI.Models.Api;

public class ApiResponseModel
{
    public MessageModel? MessageModel { get; set; }
    public object? Data { get; set; }
}

public class CreateApiResponseModelValidator : AbstractValidator<ApiResponseModel>
{
    public void ApiResponseModelValidator()
    {
        this.RuleFor(x => x.MessageModel);
        this.RuleFor(x => x.Data);
    }
}