using Project.Core.ApplicationService;
using Project.Core.ApplicationService.Commands;
using Project.Core.ApplicationService.Queries;
using Project.Product.Domain.Manufacturers;
using Project.Product.Integration.Manufacturers;
namespace Project.Product.ApplicationService.Manufacturers
{
    public class AddManufacturerQueryHandler : CommandHandler<AddManufacturerQuery, AddManufacturerQueryResult>
    {  
        private readonly IManufacturerRepository manufacturer;

        public AddManufacturerQueryHandler(IManufacturerRepository manufacturer)
        {
            this.manufacturer = manufacturer;

        }
        public override async Task<AddManufacturerQueryResult> Handle(AddManufacturerQuery request, CancellationToken cancellationToken)
        {
            //var abc = new ManufacturerInfo();
            //abc.Name = request.Name;

            //var neo = manufacturer.AddManufacturers(abc);
            using var scope = TransactionFactory.Create();
            var addmanufacturer = new ManufacturerInfo
            {
                Name = request.Name,
                Status = request.Status,
            };

            await manufacturer.AddManufacturers(addmanufacturer);

            scope.Complete();

            return new AddManufacturerQueryResult(true);
        }
    }
}
