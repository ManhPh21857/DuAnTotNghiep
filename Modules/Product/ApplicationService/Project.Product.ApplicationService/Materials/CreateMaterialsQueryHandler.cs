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
    public class CreateMaterialsQueryHandler : CommandHandler<CreateMaterialsQuery, CreateMaterialsQueryResult>
    {
        private readonly IMaterialsRepository materials;
        public CreateMaterialsQueryHandler(IMaterialsRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<CreateMaterialsQueryResult> Handle(CreateMaterialsQuery request, CancellationToken cancellationToken)
        {
            var create = new MaterialsInfo()
            {
                Name = request.Name,
            };
            await materials.CreateMaterials(create);
            return new CreateMaterialsQueryResult(true);
            
        }
    }
}
