using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.HumanResources.Domain.Employees;
using Project.HumanResources.Domain.Users;
using Project.HumanResources.Integration.Employees.Command;

namespace Project.HumanResources.ApplicationService.Employees.Command
{
    public class DeleteEmployeeCommandHandler : CommandHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResult>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUserRepository userRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUserRepository userRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
        }

        public async override Task<DeleteEmployeeCommandResult> Handle(
            DeleteEmployeeCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            foreach (var item in request.Employees)
            {
                var employee = await this.employeeRepository.GetEmployee(item.Id);

                await this.userRepository.DeleteUser(employee.UserId);

                await this.employeeRepository.DeleteEmployee(item);
            }

            scope.Complete();

            return new DeleteEmployeeCommandResult(true);
        }
    }
}
