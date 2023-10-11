using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials
{
    public class UpdateMaterialsQuery : ICommand<UpdateMaterialsQueryResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UpdateMaterialsQuery(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
