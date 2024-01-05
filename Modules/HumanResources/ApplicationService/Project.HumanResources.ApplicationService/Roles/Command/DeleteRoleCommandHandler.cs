using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles.Command;

namespace Project.HumanResources.ApplicationService.Roles.Command
{
    public class DeleteRoleCommandHandler : CommandHandler<DeleteGroupCommand, DeleteGroupCommandResult>
    {
        private readonly IRoleRepository roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async override Task<DeleteGroupCommandResult> Handle(
            DeleteGroupCommand request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();
            foreach (var item in request.Groups)
            {
                await this.roleRepository.DeleteGroup(item.Id, item.DataVersion);

                await this.roleRepository.DeleteGroupRole(item.Id);
            }

            scope.Complete();

            return new DeleteGroupCommandResult(true);
        }
    }
}
