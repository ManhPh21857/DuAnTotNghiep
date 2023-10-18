using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class DeleteTrademarkQuery : ICommand<DeleteTrademarkQueryResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DeleteTrademarkQuery(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
