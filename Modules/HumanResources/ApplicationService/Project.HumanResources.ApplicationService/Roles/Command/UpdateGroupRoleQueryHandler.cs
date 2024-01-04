using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.Domain;
using Project.HumanResources.Domain.Roles;
using Project.HumanResources.Integration.Roles.Command;
using System.Net;

namespace Project.HumanResources.ApplicationService.Roles.Command
{
    public class UpdateGroupRoleQueryHandler : CommandHandler<UpdateGroupRoleQuery, UpdateGroupRoleQueryResult>
    {
        private readonly IRoleRepository roleRepository;

        public UpdateGroupRoleQueryHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async override Task<UpdateGroupRoleQueryResult> Handle(
            UpdateGroupRoleQuery request,
            CancellationToken cancellationToken
        )
        {
            using var scope = TransactionFactory.Create();

            if (request.DataVersion.IsNullOrEmpty())
            {
                var groupId = await this.roleRepository.CreateGroup(request.Name, request.Description);
                foreach (int roleId in request.Roles)
                {
                    await this.roleRepository.CreateGroupRole(groupId, roleId);
                }
            }
            else
            {
                if (!request.Id.HasValue)
                {
                    throw new DomainException(
                        HttpStatusCode.BadRequest.GetHashCode().ToString(),
                        nameof(HttpStatusCode.BadRequest)
                    );
                }
                await this.roleRepository.UpdateGroup(
                    request.Id.Value,
                    request.Name,
                    request.Description,
                    request.DataVersion
                );

                await this.roleRepository.DeleteGroupRole(request.Id.Value);
                foreach (int roleId in request.Roles)
                {
                    await this.roleRepository.CreateGroupRole(request.Id.Value, roleId);
                }
            }

            scope.Complete();

            return new UpdateGroupRoleQueryResult(true);
        }
    }
}
