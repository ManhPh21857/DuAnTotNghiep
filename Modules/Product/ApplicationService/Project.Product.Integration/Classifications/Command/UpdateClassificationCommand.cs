using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;

namespace Project.Product.Integration.Classifications.Command
{
    public class UpdateClassificationCommand : ICommand<UpdateClassificationCommandResult>
    {
        public IEnumerable<ClassificationInfo> Classifications { get; set; }

        public UpdateClassificationCommand(IEnumerable<ClassificationInfo> classifications)
        {
            Classifications = classifications;
        }
    }
}
