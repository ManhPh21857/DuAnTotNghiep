using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class CreateOriginQuery : ICommand<CreateOriginQueryResult>
    {
        public string Name { get; set; }
        public CreateOriginQuery(string name)
        {
            this.Name = name;
        }
    }
}
