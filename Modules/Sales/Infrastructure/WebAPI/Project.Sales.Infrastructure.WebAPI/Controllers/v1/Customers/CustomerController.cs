using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Customers.Get;
using Project.Sales.Integration.Customers.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Customers
{
    public class CustomerController : SalesController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<CustomerResponseModel>>> GetCustomerl()
        {
            var result = await this.Mediator.Send(new GetCustomerQuery());

            return new ResponseBaseModel<CustomerResponseModel>
            {
                Data = result.Adapt<CustomerResponseModel>()
            };
        }
    }
}
