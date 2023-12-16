using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Sales.Infrastructure.WebAPI.Controllers.Base;
using Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers.Get;
using Project.Sales.Integration.Vouchers.Query;

namespace Project.Sales.Infrastructure.WebAPI.Controllers.v1.Vouchers
{
    public class VoucherController : SalesController
    {
        public VoucherController(ISender mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpGet("{totalPrice}")]
        public async Task<ActionResult<ResponseBaseModel<GetVoucherResponseModel>>> GetVouchers(float totalPrice)
        {
            var query = new GetVoucherQuery(totalPrice);

            var result = await this.Mediator.Send(query);

            var response = new ResponseBaseModel<GetVoucherResponseModel>
            {
                Data = result.Adapt<GetVoucherResponseModel>()
            };

            return response;
        }
    }
}
