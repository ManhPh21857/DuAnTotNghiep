using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.Base;
using Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers.Get;
using Project.HumanResources.Integration.Customers.Query;

namespace Project.HumanResources.Infrastructure.WebAPI.Controllers.v1.Customers
{
    public class CustomerController : HumanResourcesController
    {
        public CustomerController(ISender mediator) : base(mediator)
        {
        }

        [HttpGet("addresses")]
        public async Task<ActionResult<ResponseBaseModel<GetCustomerAddressResponseModel>>> GetCustomerAddress()
        {
            var query = new GetCustomerAddressQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetCustomerAddressResponseModel>
            {
                Data = result.Adapt<GetCustomerAddressResponseModel>()
            };

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<GetCustomerResponseModel>>> GetCustomer()
        {
            var query = new GetCustomerQuery();

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetCustomerResponseModel>
            {
                Data = result.Customer.Adapt<GetCustomerResponseModel>()
            };

            return response;
        }
    }
}
