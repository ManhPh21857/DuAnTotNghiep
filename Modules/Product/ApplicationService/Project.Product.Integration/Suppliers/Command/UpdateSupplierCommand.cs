using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Suppliers;

namespace Project.Product.Integration.Suppliers.Command
{
    public class UpdateSupplierCommand : ICommand<UpdateSupplierCommandResult>
    {
        public IEnumerable<SupplierInfo> Suppliers { get; set; }

        public UpdateSupplierCommand(IEnumerable<SupplierInfo> suppliers)
        {
            Suppliers = suppliers;
        }
    }
}
