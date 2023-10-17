namespace Project.Product.Integration.Suppliers.Command
{
    public class UpdateSupplierCommandResult
    {
        public bool Result { get; set; }

        public UpdateSupplierCommandResult(bool result)
        {
            Result = result;
        }
    }
}
