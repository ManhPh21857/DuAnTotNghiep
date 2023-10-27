using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;

namespace Project.Product.Integration.Materials.Command
{
    public class UpdateMaterialCommand : ICommand<UpdateMaterialCommandResult>
    {
        public IEnumerable<MaterialInfo> Materials { get; set; }

        public UpdateMaterialCommand(IEnumerable<MaterialInfo> materials)
        {
            Materials = materials;
        }
    }
}
