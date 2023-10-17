using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials;
using Project.Product.Integration.Materials.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Materials.Command
{
    public class DeleteMaterialCommandHandler : CommandHandler<DeleteMaterialCommand, DeleteMaterialCommandResult>
    {
        private readonly IMaterialRepository materials;
        public DeleteMaterialCommandHandler(IMaterialRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<DeleteMaterialCommandResult> Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            var delete = new MaterialInfo()
            {
                Id = request.Id
            };
            await materials.DeleteMaterial(delete);
            return new DeleteMaterialCommandResult(true);
        }
    }
}
