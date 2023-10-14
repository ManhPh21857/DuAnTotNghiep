using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Product.Integration.Trademarks.Command
{
    public class CreateTrademarkQuery : ICommand<CreateTrademarkQueryResult>
    {
        public string Name { get; set; }
        public CreateTrademarkQuery(string name)
        {
            this.Name = name;
        }
    }
}
