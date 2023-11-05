using Microsoft.IdentityModel.Tokens;
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Command;

namespace Project.Product.ApplicationService.Suppliers.Command
{
    public class UpdateSupplierCommandHandler : CommandHandler<UpdateSupplierCommand, UpdateSupplierCommandResult>
    {
        private readonly ISupplierRepository supplier;
        public UpdateSupplierCommandHandler(ISupplierRepository supplier)
        {
            this.supplier = supplier;

        }

        public override async Task<UpdateSupplierCommandResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Suppliers)
            {
                if (item.DataVersion.IsNullOrEmpty())
                {
                    var check = await supplier.CheckSupplierName(item.Name, item.Address);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.supplier.AddSupplier(item);
                }
                else
                {
                    var check = await supplier.CheckSupplierName(item.Name, item.Address);
                    if (check != null)
                    {
                        throw new InvalidOperationException();
                    }
                    await this.supplier.UpdateSupplier(item);
                }
            }

            return new UpdateSupplierCommandResult(true);
        }
    }
}
