using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Put
{
    public class AssignOrderRequestModel
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class AssignOrderRequestModelValidator : AbstractValidator<AssignOrderRequestModel>
    {
        public AssignOrderRequestModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
            this.RuleFor(x => x.EmployeeId).NotNull();
        }
    }
}
