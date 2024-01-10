using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Origins.Command
{
    public class DeleteOriginCommand : ICommand<DeleteOriginCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public DeleteOriginCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
