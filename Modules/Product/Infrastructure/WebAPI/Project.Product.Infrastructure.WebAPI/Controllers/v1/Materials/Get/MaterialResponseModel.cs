namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Get
{
    public class MaterialResponseModel
    {
        public IEnumerable<MaterialModel> Materials { get; set; }
        public MaterialResponseModel()
        {
            Materials = new List<MaterialModel>();  
        }
    }
}
