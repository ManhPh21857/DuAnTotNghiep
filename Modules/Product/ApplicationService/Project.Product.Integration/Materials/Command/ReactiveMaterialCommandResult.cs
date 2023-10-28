using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials.Command
{
    public class ReactiveMaterialCommandResult
    {
        public bool IsSuccess { get; set; }
        public ReactiveMaterialCommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
