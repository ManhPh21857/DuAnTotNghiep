using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Manufacturers1;
using Project.Product.Integration.Manufacturers;

namespace Project.Product.ApplicationService.Manufacturers1
{
    public class GetManufacturerQueryHandler : QueryHandler<GetManufacturerQuery,GetManufacturerQueryResult>
    {
        private readonly IManufacturerRepository manufacturer;
        public GetManufacturerQueryHandler(IManufacturerRepository manufacturer)
        {
            this.manufacturer = manufacturer;
        }

        public override async Task<GetManufacturerQueryResult> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
        {
            var result = await manufacturer.GetManufacturers();
            return new GetManufacturerQueryResult(result.ToList());
        }
    }
}
