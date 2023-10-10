using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Manufacturers;
using Project.Product.Integration.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Manufacturers
{
    public class DeleteManufacturerQueryHandler : CommandHandler<DeleteManufacturerQuery, DeleteManufacturerQueryResult>
    {
        private readonly IManufacturerRepository manufacturer;
        public DeleteManufacturerQueryHandler(IManufacturerRepository manufacturer)
        {
            this.manufacturer = manufacturer;

        }
        public async override Task<DeleteManufacturerQueryResult> Handle(DeleteManufacturerQuery request, CancellationToken cancellationToken)
        {
            var delete = new ManufacturerInfo();
            delete.Id = request.Id;
     
            await manufacturer.DeleteManufacturers(delete);
            return new DeleteManufacturerQueryResult(true);
        }
    }
}
