using FluentValidation;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Post
{
    public class UpdateAddressRequestModel
    {
        public int? Id { get; set; }
        public string? CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Commune { get; set; }
        public string? AddressDetail { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class UpdateAddressRequestModelValidator : AbstractValidator<UpdateAddressRequestModel>
    {
        public UpdateAddressRequestModelValidator()
        {
            this.RuleFor(x => x.CustomerName).NotEmpty();
            this.RuleFor(x => x.PhoneNumber).NotEmpty();
            this.RuleFor(x => x.City).NotEmpty();
            this.RuleFor(x => x.District).NotEmpty();
            this.RuleFor(x => x.Commune).NotEmpty();
            this.RuleFor(x => x.AddressDetail).NotEmpty();
        }
    }
}
