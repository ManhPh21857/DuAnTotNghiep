using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkQuery : ICommand<UpdateTrademarkQueryResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UpdateTrademarkQuery(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
