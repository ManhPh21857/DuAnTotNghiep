using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Infrastructure.WebAPI.Controllers.v1.Products.Get;
using Project.Core.Domain.Enums;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get;
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
    [HttpPost("view/{pageNo}")]
    public async Task<ActionResult<ResponseBaseModel<GetProductViewResponseModel>>> GetProductView(int pageNo, [FromBody] ProductFilter filter)
    {
        var query = new GetProductViewQuery(pageNo);

        var result = await Mediator.Send(query);

        var response = new ResponseBaseModel<GetProductViewResponseModel>
        {
            Data = result.Adapt<GetProductViewResponseModel>()
        };

        return response;
    }

    [AllowAnonymous]
    [HttpGet("item/{id}")]
    public async Task<ActionResult<ResponseBaseModel<GetProductItemResponseModel>>> GetProductItem(int id)
    {
        var query = new GetProductItemQuery(id);

        var result = await Mediator.Send(query);

        var response = new ResponseBaseModel<GetProductItemResponseModel>
        {
            Data = result.Adapt<GetProductItemResponseModel>()
        };

        return response;
    }


    [Authorize(Roles = nameof(Role.ProductView))]
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

    [Authorize(Roles = nameof(Role.ProductView))]
    [HttpGet("detail/{id}")]
    public async Task<ActionResult<ResponseBaseModel<GetProductDetailResponseModel>>> GetProduct(int id)
    {
        var query = new GetProductDetailQuery(id);

        var result = await Mediator.Send(query);

        var response = new ResponseBaseModel<GetProductDetailResponseModel>
        {
            Data = result.Adapt<GetProductDetailResponseModel>()
        };

        return response;
    }


    [Authorize(Roles = nameof(Role.ProductEdit))]
    [HttpPost]
    public async Task<ActionResult<ResponseBaseModel<UpdateProductResponseModel>>> UpdateProduct([FromBody] UpdateProductRequestModel request)
    {
        var command = request.Adapt<CreateProductCommand>();

        var result = await Mediator.Send(command);

        var response = new ResponseBaseModel<UpdateProductResponseModel>
        {
            Data = result.Adapt<UpdateProductResponseModel>()
        };

        return response;
    }

    [Authorize(Roles = nameof(Role.ProductDelete))]
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