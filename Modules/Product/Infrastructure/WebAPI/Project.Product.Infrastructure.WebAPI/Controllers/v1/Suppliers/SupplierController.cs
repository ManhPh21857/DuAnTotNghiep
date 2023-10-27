﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Product.Infrastructure.WebAPI.Controllers.Base;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Delete;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Get;
using Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers.Post;
using Project.Product.Integration.Suppliers.Query;
using Project.Product.Integration.Suppliers.Command;

namespace Project.Product.Infrastructure.WebAPI.Controllers.v1.Suppliers
{
    public class SupplierController : CommonController
    {
        private readonly IValidator<UpdateSupplierRequestModel> supplierValidator;
        public SupplierController(ISender mediator, IValidator<UpdateSupplierRequestModel> supplierValidator) : base(mediator)
        {
            this.supplierValidator = supplierValidator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ResponseBaseModel<SupplierResponseModel>>> GetSupplier()
        {
            var result = await this.Mediator.Send(new GetSupplierQuery());

            return new ResponseBaseModel<SupplierResponseModel>
            {
                Data = result.Adapt<SupplierResponseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseBaseModel<CommandBaseModel>>> UpdateSupplier(UpdateSupplierRequestModel request)
        {
            var validator = await this.supplierValidator.ValidateAsync(request);
            if (!validator.IsValid)
            {
                validator.AddToModelState(ModelState);
                return this.BadRequest(ModelState);
            }

            var command = request.Adapt<UpdateSupplierCommand>();

            var result = await this.Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("delete")]
        public async Task<ResponseBaseModel<CommandBaseModel>> DeleteSupplier(DeleteSupplierRequestModel request)
        {

            var command = request.Adapt<DeleteSupplierCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }

        [AllowAnonymous]
        [HttpPut("reactive")]
        public async Task<ResponseBaseModel<CommandBaseModel>> ReactiveSupplier(DeleteSupplierRequestModel request)
        {

            var command = request.Adapt<ReactiveSupplierCommand>();

            var result = await Mediator.Send(command);

            return new ResponseBaseModel<CommandBaseModel>
            {
                Data = result.Adapt<CommandBaseModel>()
            };
        }
    }
}
