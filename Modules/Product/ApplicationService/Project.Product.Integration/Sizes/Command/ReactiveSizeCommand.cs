using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Sizes.Command
{
    public class ReactiveSizeCommand : ICommand<ReactiveSizeCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }
        public ReactiveSizeCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
