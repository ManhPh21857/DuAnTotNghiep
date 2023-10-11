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
    public class UpdateMaterialsQueryHandler : CommandHandler<UpdateMaterialsQuery, UpdateMaterialsQueryResult>
    {
        private readonly IMaterialsRepository materials;
        public UpdateMaterialsQueryHandler(IMaterialsRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<UpdateMaterialsQueryResult> Handle(UpdateMaterialsQuery request, CancellationToken cancellationToken)
        {
            var update = new MaterialsInfo()
            {
                Id = request.Id,
                Name = request.Name
            };
            await materials.UpdateGetMaterials(update);
            return new UpdateMaterialsQueryResult(true);
        }
    }
}
