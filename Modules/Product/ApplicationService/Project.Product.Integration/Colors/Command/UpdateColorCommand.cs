using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Colors;

namespace Project.Product.Integration.Colors.Command
{
    public class UpdateColorCommand : ICommand<UpdateColorCommandResult>
    {
        public IEnumerable<ColorInfo> Colors { get; set; }

        public UpdateColorCommand(IEnumerable<ColorInfo> colors)
        {
            Colors = colors;
        }
    }
}
