using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Command;

namespace Project.Product.ApplicationService.Suppliers.Command
{
    public class AddSupplierCommandHandler : CommandHandler<AddSupplierCommand, AddSupplierCommandResult>
    {  
        private readonly ISupplierRepository supplier;

        public AddSupplierCommandHandler(ISupplierRepository supplier)
        {
            this.supplier = supplier;

        }
        public override async Task<AddSupplierCommandResult> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {

            using var scope = TransactionFactory.Create();
            var addsupplier = new SupplierInfo
            {
                Name = request.Name,
                Address = request.Address
            };
            //var check = supplier.CheckSupplierName(request.Name, request.Address);
            //if(check is not null)
            //{
            //    throw new Exception();
            //}
            await supplier.AddSupplier(addsupplier);

            scope.Complete();

            return new AddSupplierCommandResult(true);
        }
    }
}
