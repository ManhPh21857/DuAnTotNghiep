using Azure.Core;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Get;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders.Post;
using Project.Sales.Integration.Orders.Command;
using Project.Sales.Integration.Orders.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Orders
{
    public class OrderController : SalesController
    {
        public OrderController(ISender mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandSalesBase>>> CreateOrder(CreateOrderRequestModel request)
        {
            var command = request.Adapt<CreateOrderCommand>();

            var result = await this.Mediator.Send(command);

            var response = new ResponseBaseModel<CommandSalesBase>
            {
                Data = result.Adapt<CommandSalesBase>()
            };

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<GetOrderResponseModel>>> GetOrders()
        {
            var query = new GetOrderQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetOrderResponseModel>
            {
                Data = result.Adapt<GetOrderResponseModel>()
            };

            return response;
        }
    }
}
