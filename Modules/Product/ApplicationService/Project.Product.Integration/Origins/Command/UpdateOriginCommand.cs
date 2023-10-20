using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class UpdateOriginQuery : ICommand<UpdateOriginCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public UpdateOriginQuery(int id, string name, string address)
        {
            this.Id = id;
            this.Name = name;
            this.Address = address;
        }
    }
}
