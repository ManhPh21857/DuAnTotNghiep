using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Manufacturers
{
    public class DeleteManufacturerQuery : ICommand<DeleteManufacturerQueryResult>
    {
        public int Id { get; set; }

        public DeleteManufacturerQuery(int id)
        {
            this.Id = id;
        }
    }
}
