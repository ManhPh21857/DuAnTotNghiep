using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Manufacturers1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Manufacturers
{
    public class UpdateManufacturerQuery : ICommand<UpdateManufacturerQueryResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

 
        public UpdateManufacturerQuery(int id,string name,int status)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
    }
}
