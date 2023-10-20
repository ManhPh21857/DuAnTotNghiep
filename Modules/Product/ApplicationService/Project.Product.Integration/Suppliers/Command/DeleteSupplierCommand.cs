using Project.Core.ApplicationService.Commands;
namespace Project.Product.Integration.Suppliers.Command
{
    public class DeleteSupplierCommand : ICommand<DeleteSupplierCommandResult>
    {
        public int Id { get; set; }

        public DeleteSupplierCommand(int id)
        {
            this.Id = id;
        }
    }
}
