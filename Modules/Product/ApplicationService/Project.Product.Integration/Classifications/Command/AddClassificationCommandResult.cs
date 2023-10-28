using Project.Product.Domain.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications.Command
{
    public class AddClassificationCommandResult
    {
        public bool Result { get; set; }

        public AddClassificationCommandResult(bool result)
        {
            Result = result;
        }


    }
}
