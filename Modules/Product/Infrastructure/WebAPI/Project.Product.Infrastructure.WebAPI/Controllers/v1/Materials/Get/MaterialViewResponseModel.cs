namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Materials.Get
{
    public class MaterialViewResponseModel
    {
        public IEnumerable<MaterialViewModel> Materials { get; set; }
        public MaterialViewResponseModel()
        {
            this.Materials = new List<MaterialViewModel>();  
        }
    }
}
