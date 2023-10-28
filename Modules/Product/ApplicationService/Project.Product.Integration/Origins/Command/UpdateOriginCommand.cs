using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Origins;
namespace Project.Product.Integration.Origins.Command
{
    public class UpdateOriginCommand : ICommand<UpdateOriginCommandResult>
    {
        public IEnumerable<OriginInfo> Origins { get; set; }

        public UpdateOriginCommand(IEnumerable<OriginInfo> origins)
        {
            Origins = origins;
        }
    }
}
