using FluentValidation;

namespace Project.Core.Infrastructure.WebAPI.Models.Api;

public class ApiResponseListModel
{
    public MessageModel? MessageModel { get; set; }
    public DataModel? DataModel { get; set; }
}

public class CreateApiListModelValidator : AbstractValidator<ApiResponseListModel>
{
    public void ApiCreateListModelValidator()
    {
        this.RuleFor(x => x.MessageModel);
        this.RuleFor(x => x.DataModel);
    }
}