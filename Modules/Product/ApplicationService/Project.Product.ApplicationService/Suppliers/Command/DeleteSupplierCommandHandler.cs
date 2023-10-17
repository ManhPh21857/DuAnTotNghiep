using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var delete = new SupplierInfo();
            delete.Id = request.Id;
     
            await supplier.DeleteSupplier(delete);
            return new DeleteSupplierCommandResult(true);
        }
    }
}
