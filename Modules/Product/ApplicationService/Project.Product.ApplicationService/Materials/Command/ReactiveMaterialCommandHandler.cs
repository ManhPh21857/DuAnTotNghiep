
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials.Command;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class ReactiveMaterialCommandHandler : CommandHandler<ReactiveMaterialCommand, ReactiveMaterialCommandResult>
    {
        private readonly IMaterialRepository material;

        public ReactiveMaterialCommandHandler(IMaterialRepository material)
        {
            this.material = material;
        }
        public async override Task<ReactiveMaterialCommandResult> Handle(ReactiveMaterialCommand request, CancellationToken cancellationToken)
        {
            var param = new MaterialInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.material.ReActiveMaterial(param);

            return new ReactiveMaterialCommandResult(true);
        }
    }
}
