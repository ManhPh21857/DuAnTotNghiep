using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Domain.Classifications
{
    public interface IClassificationRepository
    {
        Task<IEnumerable<ClassificationInfo>> GetClassifications();
        Task AddClassifications(ClassificationInfo classification);
        Task UpdateClassifications(ClassificationInfo classification);
        Task DeleteClassifications(ClassificationInfo classification);
    }
}
