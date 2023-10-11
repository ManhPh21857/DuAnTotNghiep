using Project.Product.Domain.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials
{
    public class GetMaterialsQueryResult
    {
        public IList<MaterialsInfo> Manufacturers { get; set; }
        public GetMaterialsQueryResult(IList<MaterialsInfo> materials)
        {
            Manufacturers = materials;
        }
    }
}
