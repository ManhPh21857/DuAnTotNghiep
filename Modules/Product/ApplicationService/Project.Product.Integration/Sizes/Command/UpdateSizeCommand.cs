using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Sizes;

namespace Project.Product.Integration.Sizes.Command
{
    public class UpdateSizeCommand : ICommand<UpdateSizeCommandResult>
    {
        public IEnumerable<SizeInfo> Sizes { get; set; }

        public UpdateSizeCommand(IEnumerable<SizeInfo> sizes)
        {
            Sizes = sizes;
        }
    }
}
