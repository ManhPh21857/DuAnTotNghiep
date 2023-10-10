using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Infrastructure.WebAPI.Controllers.v1.Products.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Integration.Products;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products;

public class ProductController : CommonController
{
    public ProductController(ISender mediator) : base(mediator)
    {
    }

    [AllowAnonymous]
    [HttpGet("infos/{pageNumber}")]
    public async Task<ActionResult<ResponseBaseModel<GetProductResponseModel>>> GetProducts(int pageNumber)
    {
        var query = new GetProductQuery(pageNumber);

        var result = await Mediator.Send(query);

        var response = new ResponseBaseModel<GetProductResponseModel>
        {
            Data = result.Adapt<GetProductResponseModel>()
        };

        return response;
    }
}