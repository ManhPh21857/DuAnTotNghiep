using Project.Product.Domain.Origins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Query
{
    public class GetOriginQueryResult
    {
        public IList<OriginInfo> Origin { get; set; }
        public GetOriginQueryResult(IList<OriginInfo> origin)
        {
            Origin = origin;
        }
    }
}
