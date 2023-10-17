using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class UpdateTrademarkCommand : ICommand<UpdateTrademarkCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UpdateTrademarkCommand(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
