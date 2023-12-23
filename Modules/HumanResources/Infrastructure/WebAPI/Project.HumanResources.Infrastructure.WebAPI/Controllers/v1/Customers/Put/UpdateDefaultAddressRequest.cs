using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Put
{
    public class UpdateDefaultAddressRequest
    {
        public int? Id { get; set; }
    }

    public class UpdateDefaultAddressRequestValidator : AbstractValidator<UpdateDefaultAddressRequest>
    {
        public UpdateDefaultAddressRequestValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
        }
    }
}
