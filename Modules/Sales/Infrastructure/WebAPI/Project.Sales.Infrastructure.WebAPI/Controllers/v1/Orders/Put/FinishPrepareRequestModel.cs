using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Put
{
    public class FinishPrepareRequestModel
    {
        public int? Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class FinishPrepareRequestModelValidator : AbstractValidator<FinishPrepareRequestModel>
    {
        public FinishPrepareRequestModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
        }
    }
}
