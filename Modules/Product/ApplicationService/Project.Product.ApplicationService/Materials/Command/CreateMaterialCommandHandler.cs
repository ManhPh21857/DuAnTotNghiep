using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials.Command;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class CreateMaterialCommandHandler : CommandHandler<CreateMaterialCommand, CreateMaterialCommandResult>
    {
        private readonly IMaterialRepository materials;
        public CreateMaterialCommandHandler(IMaterialRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<CreateMaterialCommandResult> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var create = new MaterialInfo()
            {
                Name = request.Name,
            };
            await materials.CreateMaterial(create);
            return new CreateMaterialCommandResult(true);
            
        }
    }
}
