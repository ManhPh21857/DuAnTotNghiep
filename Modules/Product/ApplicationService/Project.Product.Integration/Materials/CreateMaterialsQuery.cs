using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Product.Integration.Materials
{
    public class CreateMaterialsQuery : ICommand<CreateMaterialsQueryResult>
    {
        public string Name { get; set; }
        public CreateMaterialsQuery(string name)
        {
            this.Name = name;
        }
    }

}
