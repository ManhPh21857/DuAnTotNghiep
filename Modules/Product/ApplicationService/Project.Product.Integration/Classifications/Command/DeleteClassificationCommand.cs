using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Classifications.Command
{
    public class DeleteClassificationCommand : ICommand<DeleteClassificationCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public DeleteClassificationCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
