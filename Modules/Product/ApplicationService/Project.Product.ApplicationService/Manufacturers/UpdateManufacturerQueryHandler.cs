using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Manufacturers1;
using Project.Product.Integration.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Manufacturers
{
    public class UpdateManufacturerQueryHandler : CommandHandler<UpdateManufacturerQuery, UpdateManufacturerQueryResult>
    {
        private readonly IManufacturerRepository manufacturer;
        public UpdateManufacturerQueryHandler(IManufacturerRepository manufacturer)
        {
            this.manufacturer = manufacturer;

        }

        public override async Task<UpdateManufacturerQueryResult> Handle(UpdateManufacturerQuery request, CancellationToken cancellationToken)
        {
            var update = new ManufacturerInfo();
            update.Id = request.Id;
            update.Name = request.Name;
            update.Status = request.Status;
             await manufacturer.UpdateManufacturers(update);
            return new UpdateManufacturerQueryResult(true);
        }
    }
}
