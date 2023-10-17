
namespace Project.Product.Integration.Suppliers.Command
{
    public class AddSupplierCommandResult 
    {
        public bool Result { get; set; }

        public AddSupplierCommandResult(bool result)
        {
            Result = result;
        }


    }
}
