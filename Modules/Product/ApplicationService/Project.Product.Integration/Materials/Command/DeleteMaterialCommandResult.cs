
namespace Project.Product.Integration.Materials.Command
{
    public class DeleteMaterialCommandResult
    {
        public bool Result { get; set; }
        public DeleteMaterialCommandResult(bool result)
        {
            Result = result;
        }
    }
}
