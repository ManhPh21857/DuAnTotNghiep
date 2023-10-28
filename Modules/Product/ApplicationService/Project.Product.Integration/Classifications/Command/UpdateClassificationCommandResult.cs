using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications.Command
{
    public class UpdateClassificationCommandResult
    {
        public bool Result { get; set; }

        public UpdateClassificationCommandResult(bool result)
        {
            Result = result;
        }
    }
}
