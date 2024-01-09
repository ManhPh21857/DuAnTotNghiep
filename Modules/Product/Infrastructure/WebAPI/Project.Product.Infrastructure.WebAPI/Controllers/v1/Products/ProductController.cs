using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Core.Domain;
using Project.Core.Domain.Enums;
using Project.Product.Domain.Products;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Post;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Products.Put;
using Project.Product.Integration.Products.Command;
using Project.Product.Integration.Products.Query;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Products;

public class ProductController : CommonController
{
    private readonly IValidator<ProductFilter> validatorProductFilter;
    private readonly IValidator<UpdateProductRequestModel> validatorUpdateProductRequestModel;

    public ProductController(
        ISender mediator,
        IValidator<ProductFilter> validatorProductFilter,
        IValidator<UpdateProductRequestModel> validatorUpdateProductRequestModel
    ) : base(mediator)
    {
        this.validatorProductFilter = validatorProductFilter;
        this.validatorUpdateProductRequestModel = validatorUpdateProductRequestModel;
    }

    [AllowAnonymous]
    [HttpPost("view/{pageNo}")]
    public async Task<ActionResult<ResponseBaseModel<GetProductViewResponseModel>>> GetProductView(
        int pageNo,
        [FromBody] ProductFilter filter
    )
    {
        var query = new GetProductViewQuery(pageNo, filter.Adapt<GetProductFilterParam>());

        var result = await this.Mediator.Send(query);

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

        var result = await this.Mediator.Send(query);

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

        var result = await this.Mediator.Send(query);

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

        var result = await this.Mediator.Send(query);

        var response = new ResponseBaseModel<GetProductDetailResponseModel>
        {
            Data = result.Adapt<GetProductDetailResponseModel>()
        };

        return response;
    }

    [Authorize(Roles = nameof(Role.ProductEdit))]
    [HttpPost]
    public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> UpdateProduct(
        [FromBody] UpdateProductRequestModel request
    )
    {
        var validator = await this.validatorUpdateProductRequestModel.ValidateAsync(request);
        if (!validator.IsValid)
        {
            foreach (var error in validator.Errors)
            {
                throw new DomainException(error.PropertyName, error.ErrorMessage);
            }
        }

        var command = request.Adapt<CreateProductCommand>();

        var result = await this.Mediator.Send(command);

        var response = new ResponseBaseModel<CommandProductBase>
        {
            Data = result.Adapt<CommandProductBase>()
        };

        return response;
    }

    [Authorize(Roles = nameof(Role.ProductDelete))]
    [HttpPut("delete")]
    public async Task<ActionResult<ResponseBaseModel<CommandProductBase>>> DeleteProduct(
        DeleteProductRequestModel request
    )
    {
        var command = request.Adapt<DeleteProductCommand>();

        var result = await this.Mediator.Send(command);

        var response = new ResponseBaseModel<CommandProductBase>
        {
            Data = result.Adapt<CommandProductBase>()
        };

        return response;
    }

    [AllowAnonymous]
    [HttpGet("new")]
    public async Task<ActionResult<ResponseBaseModel<GetNewProductResponseModel>>> GetNewProduct()
    {
        var query = new GetNewProductQuery();

        var result = await this.Mediator.Send(query);

        var response = new ResponseBaseModel<GetNewProductResponseModel>
        {
            Data = result.Adapt<GetNewProductResponseModel>()
        };

        return response;
    }
}
