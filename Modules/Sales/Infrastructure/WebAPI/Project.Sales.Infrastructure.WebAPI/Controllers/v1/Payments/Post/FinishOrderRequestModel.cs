using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Payments.Post
{
    public class FinishOrderRequestModel
    {
        public int? Id { get; set; }
        public string? AccessKey { get; set; }
        public string? RequestId { get; set; }
        public string? OrderId { get; set; }
    }

    public class FinishOrderRequestModelValidator : AbstractValidator<FinishOrderRequestModel>
    {
        public FinishOrderRequestModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
            this.RuleFor(x => x.AccessKey).NotEmpty();
            this.RuleFor(x => x.RequestId).NotEmpty();
            this.RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
