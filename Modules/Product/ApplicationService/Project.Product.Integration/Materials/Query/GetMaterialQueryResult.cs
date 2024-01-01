using Project.Product.Domain.Materials;
namespace Project.Product.Integration.Materials.Query
{
    public class GetMaterialQueryResult
    {
        public IEnumerable<MaterialInfo> Materials { get; set; }
        public GetMaterialQueryResult(IEnumerable<MaterialInfo> material)
        {
            this.Materials = material;
        }
    }
}
