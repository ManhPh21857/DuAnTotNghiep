using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Put
{
    public class UpdateCustomerModel
    {
        public int? Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Image { get; set; }
        public int? Sex { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateCustomerModelValidator : AbstractValidator<UpdateCustomerModel>
    {
        public UpdateCustomerModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
            this.RuleFor(x => x.LastName).NotEmpty();
            this.RuleFor(x => x.FirstName).NotEmpty();
            this.RuleFor(x => x.PhoneNumber).NotEmpty();
            this.RuleFor(x => x.Birthday).NotNull();
            this.RuleFor(x => x.Image).NotEmpty();
            this.RuleFor(x => x.Sex).NotNull().ExclusiveBetween(0, 1);
        }
    }
}
