using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications
{
    public class UpdateClassificationQuery : ICommand<UpdateClassificationQueryResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }

 
        public UpdateClassificationQuery(int id,string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
