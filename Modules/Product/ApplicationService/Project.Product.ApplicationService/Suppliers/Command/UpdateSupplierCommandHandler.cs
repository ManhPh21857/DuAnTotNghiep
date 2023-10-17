﻿using Project.Core.ApplicationService.Commands;
using Project.Product.Domain.Suppliers;
using Project.Product.Integration.Suppliers.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Product.ApplicationService.Suppliers.Command
{
    public class UpdateSupplierCommandHandler : CommandHandler<UpdateSupplierCommand, UpdateSupplierCommandResult>
    {
        private readonly ISupplierRepository supplier;
        public UpdateSupplierCommandHandler(ISupplierRepository supplier)
        {
            this.supplier = supplier;

        }

        public override async Task<UpdateSupplierCommandResult> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var update = new SupplierInfo();
            update.Id = request.Id;
            update.Name = request.Name;
            update.AddressID = request.AddressID;
            update.Status = request.Status;
             await supplier.UpdateSupplier(update);
            return new UpdateSupplierCommandResult(true);
        }
    }
}
