using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Colors.Command
{
    public class DeleteColorCommand : ICommand<DeleteColorCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public DeleteColorCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
