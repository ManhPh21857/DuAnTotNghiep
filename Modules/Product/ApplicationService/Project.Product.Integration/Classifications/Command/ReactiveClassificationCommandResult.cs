using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications.Command
{
    public class ReactiveClassificationCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveClassificationCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
