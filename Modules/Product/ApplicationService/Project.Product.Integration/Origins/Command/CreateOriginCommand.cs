using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class CreateOriginCommand : ICommand<CreateOriginCommandResult>
    {
        public string Name { get; set; }
        public CreateOriginCommand(string name)
        {
            this.Name = name;
        }
    }
}
