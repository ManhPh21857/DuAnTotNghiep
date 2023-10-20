using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Trademarks.Command
{
    public class DeleteTrademarkCommand : ICommand<DeleteTrademarkQueryResult>
    {
        public int Id { get; set; }
        public DeleteTrademarkCommand(int id)
        {
            this.Id = id;
        }
    }
}
