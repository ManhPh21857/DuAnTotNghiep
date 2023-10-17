using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors.Get;
using Project.Product.Integration.Colors;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Colors
{
    public class ColorController : CommonController
    {
        public ColorController(ISender mediator) : base(mediator)
        {
            
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ResponseBaseModel<ColorResponseModel>> GetColors()
        {
            var result = await  Mediator.Send(new GetColorQuery());

            return new ResponseBaseModel<ColorResponseModel>
            {
                Data = result.Adapt<ColorResponseModel>()
            };
        }
    }
}
