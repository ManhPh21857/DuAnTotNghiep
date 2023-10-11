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
        Task CreateGetMaterials(MaterialsInfo materials);
        Task UpdateGetMaterials(MaterialsInfo materials);
        Task DeleteGetMaterials(MaterialsInfo materials);
    }
}
