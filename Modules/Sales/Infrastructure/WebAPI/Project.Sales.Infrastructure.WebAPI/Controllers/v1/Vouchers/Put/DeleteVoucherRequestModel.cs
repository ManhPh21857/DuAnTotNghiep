using FluentValidation;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Put
{
    public class DeleteVoucherRequestModel
    {
        public int? Id { get; set; }
        public byte[]? DataVersion { get; set; }
    }

    public class DeleteVoucherRequestModelValidator : AbstractValidator<DeleteVoucherRequestModel>
    {
        public DeleteVoucherRequestModelValidator()
        {
            this.RuleFor(x => x.Id).NotNull();
        }
    }
}
