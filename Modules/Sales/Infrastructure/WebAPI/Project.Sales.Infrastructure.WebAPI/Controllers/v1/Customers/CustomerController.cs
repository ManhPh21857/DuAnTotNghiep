using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Customers.Get;
using Project.Sales.Integration.Customers.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Customers
{
    public class CustomerController : SalesController
    {
        public CustomerController(ISender mediator) : base(mediator)
        {
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<CustomerResponseModel>>> GetOrders()
        {
            var query = new GetCustomerQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<CustomerResponseModel>
            {
                Data = result.Adapt<CustomerResponseModel>()
            };

            return response;
        }
    }
}
