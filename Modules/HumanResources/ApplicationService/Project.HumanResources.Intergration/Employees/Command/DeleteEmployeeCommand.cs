using Project.Core.ApplicationService.Commands;
using Project.HumanResources.Domain.Employees;

namespace Project.HumanResources.Integration.Employees.Command
{
    public class DeleteEmployeeCommand : ICommand<DeleteEmployeeCommandResult>
    {
        public IEnumerable<DeleteEmployeeParam> Employees { get; set; }
        
        public DeleteEmployeeCommand(IEnumerable<DeleteEmployeeParam> employees)
        {
            this.Employees = employees;
        }
    }
}
