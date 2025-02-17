﻿using Project.Core.ApplicationService.Commands;
using Project.Sales.Domain.SaleCounters;

namespace Project.Sales.Integration.SaleCounters.Command
{
    public class CreateOrderDetailCommand : ICommand<CreateOrderDetailCommandResult>
    {
        //Order
        public OrderInfo Order { get; set; }
        public IEnumerable<OrderDetailInfo> Orderdetails { get; set; }

        public CreateOrderDetailCommand(
            OrderInfo order,
            IEnumerable<OrderDetailInfo> orderdetails)
        
        {
            Order = order;
            Orderdetails = orderdetails;
        }
    }
}
