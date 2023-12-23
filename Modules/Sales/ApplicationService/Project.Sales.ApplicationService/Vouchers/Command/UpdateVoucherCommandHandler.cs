using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Employees;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.Vouchers.Command;

namespace Project.Sales.ApplicationService.Vouchers.Command
{
    public class UpdateVoucherCommandHandler : CommandHandler<UpdateVoucherCommand, UpdateVoucherCommandResult>
    {
        private readonly IVoucherRepository voucherRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ISessionInfo sessionInfo;

        public UpdateVoucherCommandHandler(
            IVoucherRepository voucherRepository,
            IEmployeeRepository employeeRepository,
            ISessionInfo sessionInfo
        )
        {
            this.voucherRepository = voucherRepository;
            this.employeeRepository = employeeRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateVoucherCommandResult> Handle(
            UpdateVoucherCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            int userId = this.sessionInfo.UserId.value;
            int? employeeId = await this.employeeRepository.GetEmployeeId(userId);
            if (employeeId is null)
            {
                throw new DomainException("", "Nhân viên không tồn tại");
            }

            if (request.DataVersion.IsNullOrEmpty())
            {
                //create
                await this.voucherRepository.CreateVoucher(new CreateVoucherParam
                    {
                        Name = request.Name,
                        VoucherType = request.VoucherType,
                        MinimumPrice = request.MinimumPrice,
                        Discount = request.Discount,
                        DiscountType = request.DiscountType,
                        MaximumDiscount = request.MaximumDiscount,
                        ApplyPeriodStart = request.ApplyPeriodStart,
                        ApplyPeriodEnd = request.ApplyPeriodEnd,
                        Quantity = request.Quantity,
                        CreatedBy = employeeId.Value,
                        LastUpdatedBy = employeeId.Value
                    }
                );
            }
            else
            {
                //update
                await this.voucherRepository.UpdateVoucher(new UpdateVoucherParam
                    {
                        Id = request.Id,
                        Name = request.Name,
                        VoucherType = request.VoucherType,
                        MinimumPrice = request.MinimumPrice,
                        Discount = request.Discount,
                        DiscountType = request.DiscountType,
                        MaximumDiscount = request.MaximumDiscount,
                        ApplyPeriodStart = request.ApplyPeriodStart,
                        ApplyPeriodEnd = request.ApplyPeriodEnd,
                        Quantity = request.Quantity,
                        LastUpdatedBy = employeeId.Value,
                        DataVersion = request.DataVersion
                    }
                );
            }

            scope.Complete();
            return new UpdateVoucherCommandResult(true);
        }
    }
}
