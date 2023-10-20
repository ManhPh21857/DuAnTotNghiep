using Azure.Core;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Infrastructure.WebAPI.Controllers.v1.Products.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post;
using Project.Product.Integration.Products.Command;
using Project.Product.Integration.Products.Query;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products;

public class ProductController : CommonController
{
    public ProductController(ISender mediator) : base(mediator)
    {
    }

    [AllowAnonymous]
    [HttpGet("{pageNo}")]
    public async Task<ActionResult<ResponseBaseModel<GetProductResponseModel>>> GetProducts(int pageNo)
    {
        var query = new GetProductQuery(pageNo);

        var result = await Mediator.Send(query);

        var response = new ResponseBaseModel<GetProductResponseModel>
        {
            Data = result.Adapt<GetProductResponseModel>()
        };

        return response;
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

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseBaseModel<DeleteProductResponseModel>>> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand(id);

        var result = await Mediator.Send(command);

        var response = new ResponseBaseModel<DeleteProductResponseModel>
        {
            Data = result.Adapt<DeleteProductResponseModel>()
        };

        return response;
    }
}