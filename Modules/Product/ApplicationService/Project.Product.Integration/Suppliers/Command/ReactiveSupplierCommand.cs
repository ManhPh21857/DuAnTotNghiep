
using Project.Core.ApplicationService.Commands;

namespace Project.Product.Integration.Suppliers.Command
{
    public class ReactiveSupplierCommand : ICommand<ReactiveSupplierCommandResult>
    {
        public int Id { get; set; }
        public byte[]? DataVersion { get; set; }

        public ReactiveSupplierCommand(int id, byte[]? dataVersion)
        {
            this.Id = id;
            this.DataVersion = dataVersion;
        }
    }
}
