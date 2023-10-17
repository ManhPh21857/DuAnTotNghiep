
namespace Project.Product.Integration.Materials.Command
{
    public class CreateMaterialCommandResult
    {
        public bool Result { get; set; }
        public CreateMaterialCommandResult(bool result)
        {
            Result = result;
        }
    }
}
