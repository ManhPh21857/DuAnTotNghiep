using Microsoft.IdentityModel.Tokens;
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
        public async override  Task<UpdateMaterialCommandResult> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Materials)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    await this.material.CreateMaterial(item);
                }
                else
                {
                    await this.material.UpdateMaterial(item);
                }
            }

            return new UpdateMaterialCommandResult(true);
        }
    }
}
