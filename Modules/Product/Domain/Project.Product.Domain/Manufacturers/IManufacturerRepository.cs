using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Manufacturers
{
    public interface IManufacturerRepository
    {
        Task<IEnumerable<ManufacturerInfo>> GetManufacturers();
        Task AddManufacturers(ManufacturerInfo manufacturer);
        Task UpdateManufacturers(ManufacturerInfo manufacturer);
        Task DeleteManufacturers(ManufacturerInfo manufacturer);
    }
}
