using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Customers.Command;

namespace Project.HumanResources.ApplicationService.Customers.Command
{
    public class UpdateAddressDefaultCommandHandler
        : CommandHandler<UpdateAddressDefaultCommand, UpdateAddressDefaultCommandResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public UpdateAddressDefaultCommandHandler(ICustomerRepository customerRepository, ISessionInfo sessionInfo)
        {
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateAddressDefaultCommandResult> Handle(
            UpdateAddressDefaultCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();
            int userId = this.sessionInfo.UserId.value;
            int? customerId = await this.customerRepository.GetCustomerId(userId);

            if (customerId is null)
            {
                throw new DomainException("", "Khách hàng không tồn tại");
            }

            await this.customerRepository.UpdateDefaultAddress(customerId.Value, request.Id);

            scope.Complete();

            return new UpdateAddressDefaultCommandResult(true);
        }
    }
}
