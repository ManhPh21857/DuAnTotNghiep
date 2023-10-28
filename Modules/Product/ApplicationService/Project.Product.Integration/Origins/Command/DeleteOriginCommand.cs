﻿using Project.Core.ApplicationService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.Integration.Origins.Command
{
    public class DeleteOriginCommand : ICommand<DeleteOriginCommandResult>
    {
        public int Id { get; set; }
        public DeleteOriginCommand(int id)
        {
            this.Id = id;
        }
    }
}
