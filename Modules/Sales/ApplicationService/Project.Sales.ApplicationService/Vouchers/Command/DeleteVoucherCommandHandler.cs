using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Employees;
using Project.Sales.Domain.Vouchers;
using Project.Sales.Integration.Vouchers.Command;

namespace Project.Sales.ApplicationService.Vouchers.Command
{
    public class DeleteVoucherCommandHandler : CommandHandler<DeleteVoucherCommand, DeleteVoucherCommandResult>
    {
        private readonly IVoucherRepository voucherRepository;
        private readonly ISessionInfo sessionInfo;
        private readonly IEmployeeRepository employeeRepository;

        public DeleteVoucherCommandHandler(
            IVoucherRepository voucherRepository,
            ISessionInfo sessionInfo,
            IEmployeeRepository employeeRepository
        )
        {
            this.voucherRepository = voucherRepository;
            this.sessionInfo = sessionInfo;
            this.employeeRepository = employeeRepository;
        }
        public async override Task<DeleteVoucherCommandResult> Handle(
            DeleteVoucherCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            int userId = this.sessionInfo.UserId.value;
            int? employeeIId = await this.employeeRepository.GetEmployeeId(userId);

            if (employeeIId is null)
            {
                throw new DomainException("", "Nhân viên không tồn tại");
            }


            if (request.IsDelete)
            {
                await this.voucherRepository.DeleteVoucher(
                    request.Id,
                    0,
                    1,
                    request.DataVersion,
                    employeeIId.Value
                );
            }
            else
            {
                await this.voucherRepository.DeleteVoucher(
                    request.Id,
                    1,
                    0,
                    request.DataVersion,
                    employeeIId.Value
                );
            }

            scope.Complete();
            return new DeleteVoucherCommandResult(true);
        }
    }
}
