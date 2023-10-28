using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials.Command;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class DeleteMaterialCommandHandler : CommandHandler<DeleteMaterialCommand, DeleteMaterialCommandResult>
    {
        private readonly IMaterialRepository materialRepository;
        public DeleteMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        public override async Task<DeleteMaterialCommandResult> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            var param = new MaterialInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.materialRepository.DeleteMaterial(param);

            return new DeleteMaterialCommandResult(true);
        }
    }
}
