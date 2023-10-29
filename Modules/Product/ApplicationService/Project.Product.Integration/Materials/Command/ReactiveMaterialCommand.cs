
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Materials.Command
{
    public class ReactiveMaterialCommand : ICommand<ReactiveMaterialCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public ReactiveMaterialCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
