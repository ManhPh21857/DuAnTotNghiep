using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials;
using Project.Product.Integration.Materials.Command;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class UpdateMaterialCommandHandler : CommandHandler<UpdateMaterialCommand, UpdateMaterialCommandResult>
    {
        private readonly IMaterialRepository material;

        public UpdateMaterialCommandHandler(IMaterialRepository material)
        {
            this.material = material;
        }
        public override async Task<UpdateMaterialCommandResult> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var update = new MaterialInfo()
            {
                Id = request.Id,
                Name = request.Name
            };

             var check = await material.CheckMaterialName(request.Name);
            if(check is not null)
            {
                throw new Exception();
            }

            await material.UpdateMaterial(update);
            
            
            return new UpdateMaterialCommandResult(true);
        }
    }
}
