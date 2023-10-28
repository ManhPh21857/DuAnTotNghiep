
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Origins.Command
{
    public class ReactiveOriginCommand : ICommand<ReactiveOriginCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public ReactiveOriginCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
