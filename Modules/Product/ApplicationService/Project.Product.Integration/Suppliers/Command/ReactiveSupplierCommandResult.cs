
namespace Project.Product.Integration.Suppliers.Command
{
    public class ReactiveSupplierCommandResult
    {
        public bool Result { get; set; }
        public ReactiveSupplierCommandResult(bool result)
        {
            Result = result;
        }
    }
}
