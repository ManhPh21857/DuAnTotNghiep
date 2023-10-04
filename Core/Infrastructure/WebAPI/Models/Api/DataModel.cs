using FluentValidation;

namespace Project.Core.Infrastructure.WebAPI.Models.Api;

public class DataModel
{
    public object List { get; set; }
    public int TotalRow { get; set; }
}

public class CreateDataModelValidator : AbstractValidator<DataModel>
{
    public void DataCreateModelValidator()
    {
        this.RuleFor(x => x.List);
        this.RuleFor(x => x.TotalRow);
    }
}