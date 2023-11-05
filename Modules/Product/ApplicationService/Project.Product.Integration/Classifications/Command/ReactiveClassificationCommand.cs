
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Classifications.Command
{
    public class ReactiveClassificationCommand : ICommand<ReactiveClassificationCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public ReactiveClassificationCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
