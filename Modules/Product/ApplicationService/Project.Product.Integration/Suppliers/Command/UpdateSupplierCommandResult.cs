namespace Project.Product.Integration.Suppliers.Command
{
    public class UpdateSupplierCommandResult
    {
        public bool IsSuccess { get; set; }

        public UpdateSupplierCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
