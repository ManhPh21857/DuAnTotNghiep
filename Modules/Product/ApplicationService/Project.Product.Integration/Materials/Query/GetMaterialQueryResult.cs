using Project.Product.Domain.Materials;
namespace Project.Product.Integration.Materials.Query
{
    public class GetMaterialQueryResult
    {
        public IList<MaterialInfo> Materials { get; set; }
        public GetMaterialQueryResult(IList<MaterialInfo> material)
        {
            Materials = material;
        }
    }
}
