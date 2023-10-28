using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials.Command;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class UpdateMaterialCommandHandler : CommandHandler<UpdateMaterialCommand, UpdateMaterialCommandResult>
    {
        private readonly IMaterialRepository materialRepository;

        public UpdateMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        public async override  Task<UpdateMaterialCommandResult> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {          
            foreach (var item in request.Materials)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    var check = await this.materialRepository.CheckMaterialName(item.Name);
                    if(check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.materialRepository.CreateMaterial(item);
                }
                else
                {
                    var check = await this.materialRepository.CheckMaterialName(item.Name);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.materialRepository.UpdateMaterial(item);
                }
            }
            return new UpdateMaterialCommandResult(true);
        }
    }
}
