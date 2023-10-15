using Project.Product.Domain.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications
{
    public class AddClassificationQueryResult
    {
        public bool Result { get; set; }

        public AddClassificationQueryResult(bool result)
        {
            Result = result;
        }


    }
}
