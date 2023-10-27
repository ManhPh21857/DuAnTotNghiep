using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Command;
namespace Project.Product.ApplicationService.Suppliers.Command
{
    public class DeleteSupplierCommandHandler : CommandHandler<DeleteSupplierCommand, DeleteSupplierCommandResult>
    {
        private readonly ISupplierRepository supplier;
        public DeleteSupplierCommandHandler(ISupplierRepository supplier)
        {
            this.supplier = supplier;

        }
        public async override Task<DeleteSupplierCommandResult> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var param = new SupplierInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.supplier.DeleteSupplier(param);

            return new DeleteSupplierCommandResult(true);
        }
    }
}
