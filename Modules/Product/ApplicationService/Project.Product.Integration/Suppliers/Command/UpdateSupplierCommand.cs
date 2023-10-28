using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Suppliers.Command
{
    public class UpdateSupplierCommand : ICommand<UpdateSupplierCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }

 
        public UpdateSupplierCommand(int id,string name,int status, string address)
        {

            this.Id = id;
            this.Name = name;
            this.Address = address;
            this.Status = status;
        }
    }
}
