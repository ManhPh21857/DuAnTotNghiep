using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post;
using Project.Product.Integration.Products.Command;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products;

public class ProductController : CommonController
{
    public ProductController(ISender mediator) : base(mediator)
    {
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<ResponseBaseModel<CreateProductResponseModel>>> CreateProduct([FromBody] CreateProductRequestModel request)
    {
        var command = request.Adapt<CreateProductCommand>();

        var result = await Mediator.Send(command);

        var response = new ResponseBaseModel<CreateProductResponseModel>
        {
            Data = result.Adapt<CreateProductResponseModel>()
        };

        return response;
    }
}