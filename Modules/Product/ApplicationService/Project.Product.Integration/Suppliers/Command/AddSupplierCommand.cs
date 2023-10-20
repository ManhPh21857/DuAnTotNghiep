using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Suppliers.Command
{
    public class AddSupplierCommand : ICommand<AddSupplierCommandResult>
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }


        public AddSupplierCommand(

            string name,
            string address,
            int status
        )
        {

            this.Name = name;
            this.Address = address;
            this.Status = status;
        }
    }
}
