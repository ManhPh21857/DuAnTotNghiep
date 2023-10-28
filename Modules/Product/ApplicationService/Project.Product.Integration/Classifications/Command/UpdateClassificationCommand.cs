using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Classifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Classifications.Command
{
    public class UpdateClassificationCommand : ICommand<UpdateClassificationCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public UpdateClassificationCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
