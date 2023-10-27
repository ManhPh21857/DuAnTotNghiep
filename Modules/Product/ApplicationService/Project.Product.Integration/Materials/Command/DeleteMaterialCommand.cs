using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Materials.Command
{
    public class DeleteMaterialCommand : ICommand<DeleteMaterialCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public DeleteMaterialCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
