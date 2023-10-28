using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Materials.Command
{
    public class DeleteMaterialCommand : ICommand<DeleteMaterialCommandResult>
    {
        public int Id { get; set; }
        public DeleteMaterialCommand(int id)
        {
            this.Id = id;
        }
    }
}
