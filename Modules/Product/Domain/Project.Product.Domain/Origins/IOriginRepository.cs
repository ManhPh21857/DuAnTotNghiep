using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Origins
{
    public interface IOriginRepository
    {
        Task<IEnumerable<OriginInfo>> GetOrigin(int? id);
        Task CreateOrigin(OriginInfo origin);
        Task UpdateOrigin(OriginInfo origin);
        Task DeleteOrigin(OriginInfo origin);
        Task ReactiveOrigin(OriginInfo origin);
        Task<OriginInfo> CheckOriginName(string name);
    }
}
