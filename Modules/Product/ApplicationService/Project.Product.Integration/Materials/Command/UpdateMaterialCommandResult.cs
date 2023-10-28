
namespace Project.Product.Integration.Materials.Command
{
    public class UpdateMaterialCommandResult
    {
        public bool Result { get; set; }
        public UpdateMaterialCommandResult(bool result)
        {
            Result = result;
        }
    }
}
