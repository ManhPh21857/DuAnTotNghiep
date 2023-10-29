using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Colors;
using Project.Product.Domain.Sizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Sizes.Command
{
    public class UpdateSizeCommand : ICommand<UpdateSizeCommandResult>
    {
        public IEnumerable<SizeInfo> Sizes { get; set; }

        public UpdateSizeCommand(IEnumerable<SizeInfo> sizes)
        {
            Sizes = sizes;
        }
    }
}
