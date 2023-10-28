using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications.Command
{
    public class DeleteClassificationCommand : ICommand<DeleteClassificationCommandResult>
    {
        public int Id { get; set; }

        public DeleteClassificationCommand(int id)
        {
            Id = id;
        }
    }
}
