using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Materials
{
    public class GetMaterialsQueryHandler : QueryHandler<GetMaterialsQuery, GetMaterialsQueryResult>
    {
        private readonly IMaterialsRepository materials;
        public GetMaterialsQueryHandler(IMaterialsRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<GetMaterialsQueryResult> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
        {
            var result = await materials.GetMaterials();
            return new GetMaterialsQueryResult(result.ToList());
        }
    }
}
