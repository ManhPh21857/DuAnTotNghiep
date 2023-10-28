namespace Project.Product.Integration.Suppliers.Command
{
    public class DeleteSupplierCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteSupplierCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
