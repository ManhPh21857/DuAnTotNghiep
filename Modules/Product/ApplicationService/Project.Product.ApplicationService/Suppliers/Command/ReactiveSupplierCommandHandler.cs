
using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Command;

namespace Project.Product.ApplicationService.Suppliers.Command
{
    public class ReactiveSupplierCommandHandler : CommandHandler<ReactiveSupplierCommand, ReactiveSupplierCommandResult>
    {
        private readonly ISupplierRepository supplierRepository;

        public ReactiveSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public async override Task<ReactiveSupplierCommandResult> Handle(ReactiveSupplierCommand request, CancellationToken cancellationToken)
        {
            var param = new SupplierInfo { Id = request.Id, DataVersion = request.DataVersion };

            await this.supplierRepository.ReactiveSupplier(param);

            return new ReactiveSupplierCommandResult(true);
        }
    }
}
