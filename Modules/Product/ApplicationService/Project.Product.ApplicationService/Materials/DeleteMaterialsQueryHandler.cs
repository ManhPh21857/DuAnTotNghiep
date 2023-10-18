using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Materials
{
    public class DeleteMaterialsQueryHandler : CommandHandler<DeleteMaterialsQuery, DeleteMaterialsQueryResult>
    {
        private readonly IMaterialsRepository materials;
        public DeleteMaterialsQueryHandler(IMaterialsRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<DeleteMaterialsQueryResult> Handle(DeleteMaterialsQuery request, CancellationToken cancellationToken)
        {
            var delete = new MaterialsInfo()
            {
                Id = request.Id
            };
            await materials.DeleteMaterials(delete);
            return new DeleteMaterialsQueryResult(true);
        }
    }
}
