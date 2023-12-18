using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Customers;
using Project.HumanResources.Integration.Customers.Command;

namespace Project.HumanResources.ApplicationService.Customers.Command
{
    public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, UpdateCustomerCommandResult>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ISessionInfo sessionInfo;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ISessionInfo sessionInfo)
        {
            this.customerRepository = customerRepository;
            this.sessionInfo = sessionInfo;
        }

        public async override Task<UpdateCustomerCommandResult> Handle(
            UpdateCustomerCommand request,
            CancellationToken cancellationToken
        )
        {
            var param = new UpdateCustomerParam
            {
                Id = request.Id,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                Birthday = request.Birthday,
                Image = request.Image,
                Sex = request.Sex,
                DataVersion = request.DataVersion
            };

            await this.customerRepository.UpdateCustomer(param);

            return new UpdateCustomerCommandResult(true);
        }
    }
}
