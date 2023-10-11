using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Materials
{
    public interface IMaterialsRepository
    {
        Task<IEnumerable<MaterialsInfo>> GetMaterials();
        Task CreateMaterials(MaterialsInfo materials);
        Task UpdateMaterials(MaterialsInfo materials);
        Task DeleteMaterials(MaterialsInfo materials);
    }
}
