using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Colors.Command
{
    public class ReactiveColorCommand : ICommand<ReactiveColorCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public ReactiveColorCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
