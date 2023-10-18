using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class DeleteOriginQuery : ICommand<DeleteOriginQueryResult>
    {
        public int Id { get; set; }
        public DeleteOriginQuery(int id)
        {
            this.Id = id;
        }
    }
}
