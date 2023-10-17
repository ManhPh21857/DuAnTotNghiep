using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Suppliers.Command
{
    public class UpdateSupplierCommand : ICommand<UpdateSupplierCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressID { get; set; }
        public int Status { get; set; }

 
        public UpdateSupplierCommand(int id,string name,int status, int addressID)
        {

            this.Id = id;
            this.Name = name;
            this.AddressID = addressID;
            this.Status = status;
        }
    }
}
