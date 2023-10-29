using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Sizes.Command
{
    public class ReactiveSizeCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveSizeCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
