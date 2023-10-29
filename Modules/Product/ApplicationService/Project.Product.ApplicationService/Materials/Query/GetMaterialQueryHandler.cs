using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Materials;
using Project.Product.Integration.Materials.Query;

namespace Project.Product.ApplicationService.Materials.Query
{
    public class GetMaterialQueryHandler : QueryHandler<GetMaterialQuery, GetMaterialQueryResult>
    {
        private readonly IMaterialRepository materials;
        public GetMaterialQueryHandler(IMaterialRepository materials)
        {
            this.materials = materials;
        }
        public override async Task<GetMaterialQueryResult> Handle(GetMaterialQuery request, CancellationToken cancellationToken)
        {
            var result = await materials.GetMaterial(null);
            return new GetMaterialQueryResult(result.ToList());
        }
    }
}
