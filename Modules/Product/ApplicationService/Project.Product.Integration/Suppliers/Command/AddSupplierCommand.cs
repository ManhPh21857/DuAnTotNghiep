using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Suppliers.Command
{
    public class AddSupplierCommand : ICommand<AddSupplierCommandResult>
    {

        public string Name { get; set; }
        public int AddressID { get; set; }
        public int Status { get; set; }


        public AddSupplierCommand(

            string name,
            int addressID,
            int status
        )
        {

            this.Name = name;
            this.AddressID = addressID;
            this.Status = status;
        }
    }
}
