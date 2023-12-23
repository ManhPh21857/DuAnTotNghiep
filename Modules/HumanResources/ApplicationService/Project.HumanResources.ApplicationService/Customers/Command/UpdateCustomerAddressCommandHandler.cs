using Mapster;
using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Customers.Command;

namespace Project.HumanResources.ApplicationService.Customers.Command
{
    public class UpdateCustomerAddressCommandHandler
        : CommandHandler<UpdateCustomerAddressCommand, UpdateCustomerAddressCommandResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public UpdateCustomerAddressCommandHandler(ICustomerRepository customerRepository, ISessionInfo sessionInfo)
        {
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateCustomerAddressCommandResult> Handle(
            UpdateCustomerAddressCommand request,
            CancellationToken cancellationToken
        )
        {
            int userId = this.sessionInfo.UserId.value;
            int? customerId = await this.customerRepository.GetCustomerId(userId);

            if (customerId is null)
            {
                throw new DomainException("", "Khách hàng không tồn tại");
            }

            var param = request.Adapt<UpdateCustomerAddressParam>();
            param.CustomerId = customerId.Value;

            if (param.DataVersion.IsNullOrEmpty())
            {
                await this.customerRepository.InsertCustomerAddress(param);
            }
            else
            {
                await this.customerRepository.UpdateCustomerAddress(param);
            }

            return new UpdateCustomerAddressCommandResult(true);
        }
    }
}
