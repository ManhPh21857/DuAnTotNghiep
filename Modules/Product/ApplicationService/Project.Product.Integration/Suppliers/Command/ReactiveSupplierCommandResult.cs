
namespace Project.Product.Integration.Suppliers.Command
{
    public class ReactiveSupplierCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveSupplierCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
