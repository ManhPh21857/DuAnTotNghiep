using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Materials
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<MaterialInfo>> GetMaterial(int? id);
        Task CreateMaterial(MaterialInfo material);
        Task UpdateMaterial(MaterialInfo material);
        Task DeleteMaterial(MaterialInfo material);
        Task ReActiveMaterial(MaterialInfo material);
        Task<MaterialInfo> CheckMaterialName(string name);
    }
}
