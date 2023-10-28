using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class DeleteOriginCommandResult
    {
        public bool IsSuccess { get; set; }
        public DeleteOriginCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
