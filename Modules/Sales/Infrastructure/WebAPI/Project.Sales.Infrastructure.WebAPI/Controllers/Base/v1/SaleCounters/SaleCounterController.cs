using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters.Get;
using Project.Sales.Integration.SaleCounters.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.Base.v1.SaleCounters
{
    public class SaleCounterController : SalesController
    {
        public SaleCounterController(ISender mediator) : base(mediator)
        {

        }
    
        [AllowAnonymous]
        [HttpPost("view/{pageNo}")]
        public async Task<ActionResult<ResponseBaseModel<GetSaleCounterResponseModel>>> GetSaleCounter(int pageNo, [FromBody] SaleCounterFilter filter)
        {
            var query = new GetSaleCounterQuery(pageNo);

            var result = await Mediator.Send(query);

            var response = new ResponseBaseModel<GetSaleCounterResponseModel>
            {
                Data = result.Adapt<GetSaleCounterResponseModel>()
            };

            return response;
        }

    }
}
