using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Sizes.Command
{
    public class DeleteSizeCommandResult
    {
        public bool IsSuccess { get; set; }

        public DeleteSizeCommandResult(bool isSuccess)
        {
            this.IsSuccess = isSuccess;
        }
    }
}
