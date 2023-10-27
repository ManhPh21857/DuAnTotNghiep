using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Query;

namespace Project.Product.ApplicationService.Suppliers.Query
{
    public class GetSupplierQueryHandler : QueryHandler<GetSupplierQuery,GetSupplierQueryResult>
    {
        private readonly ISupplierRepository supplier;
        public GetSupplierQueryHandler(ISupplierRepository supplier)
        {
            this.supplier = supplier;

        }

        public override async Task<GetSupplierQueryResult> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {

            var result = await supplier.GetSupplier(null);
           
            return new GetSupplierQueryResult(result.ToList());
        }
    }
}
