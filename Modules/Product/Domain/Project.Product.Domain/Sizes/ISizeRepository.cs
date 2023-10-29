using Project.Product.Domain.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Sizes
{
    public interface ISizeRepository
    {
        Task<IEnumerable<SizeInfo>> GetSizes(int? id);
        Task CreateSize(SizeInfo size);
        Task UpdateSize(SizeInfo size);
        Task DeleteSize(SizeInfo size);
        Task ReActiveSize(SizeInfo size);
    }
}
