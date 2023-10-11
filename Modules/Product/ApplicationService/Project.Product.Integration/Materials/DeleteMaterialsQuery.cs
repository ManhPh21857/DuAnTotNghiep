using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Materials
{
    public class DeleteMaterialsQuery : ICommand<DeleteMaterialsQueryResult>
    {
        public int Id { get; set; }
        public DeleteMaterialsQuery(int id)
        {
            this.Id = id;
        }
    }
}
