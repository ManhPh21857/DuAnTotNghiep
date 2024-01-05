using Project.Core.ApplicationService.Commands;
using Project.HumanResources.Domain.Roles;

namespace Project.HumanResources.Integration.Roles.Command
{
    public class DeleteGroupCommand : ICommand<DeleteGroupCommandResult>
    {
        public IEnumerable<DeleteGroupInfo> Groups { get; set; }

        public DeleteGroupCommand(IEnumerable<DeleteGroupInfo> groups)
        {
            this.Groups = groups;
        }
    }
}
