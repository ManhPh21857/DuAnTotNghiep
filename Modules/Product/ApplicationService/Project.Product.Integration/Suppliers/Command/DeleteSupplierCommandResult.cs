namespace Project.Product.Integration.Suppliers.Command
{
    public class DeleteSupplierCommandResult
    {
        public bool Result { get; set; }

        public DeleteSupplierCommandResult(bool result)
        {
            Result = result;
        }
    }
}
