
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials.Command;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class ReactiveMaterialCommandHandler : CommandHandler<ReactiveMaterialCommand, ReactiveMaterialCommandResult>
    {
        private readonly IMaterialRepository materialRepository;

        public ReactiveMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        public async override Task<ReactiveMaterialCommandResult> Handle(ReactiveMaterialCommand request, CancellationToken cancellationToken)
        {
            var param = new MaterialInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.materialRepository.ReActiveMaterial(param);

            return new ReactiveMaterialCommandResult(true);
        }


    }
}
